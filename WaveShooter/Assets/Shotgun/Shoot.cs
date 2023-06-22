using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    Transform playerCamera;
    
    [Header("Shotgun Attributes")]
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float cooldownTime;
    [SerializeField] float reloadTime;
    [SerializeField] int maxAmmo;
    [SerializeField] float maxSpread;
    [SerializeField] int pelletCount;

    [Header("States")]
    bool coolingDown = false;
    bool reloading = false;
    int currentShots;

    float timer = 0;

    [SerializeField] LayerMask playerLayer;
    int enemyLayer; // Layer var needs to be int to be read by raycast
    [SerializeField] Transform bulletSpawnPoint;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI ammoCountText;
    
    [Header("Audio")]
    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioSource reloadSound;
    
    [Header("VFX")]
    
    [Header("Keycodes")]
    [SerializeField] KeyCode reloadButton;

    public delegate void PlayerShoot();
    public static PlayerShoot playerShoot;
    
    // Start is called before the first frame update
    void Start() {
        enemyLayer = LayerMask.NameToLayer("Enemy"); // Establish the enemy layer
        playerShoot += FireGun; // Add the FireGun method to the playerShoot event
        playerCamera = transform.parent.transform;
        currentShots = maxAmmo;
        UpdateUI();
        Debug.Log(currentShots);
    }

    // Update is called once per frame
    void Update() {
        // Debug.Log(timer);

        if (Input.GetMouseButtonDown(0) && !coolingDown && !reloading && currentShots > 0) {
            playerShoot?.Invoke(); // Fire the playerShoot event to all delegates
        }

        if (Input.GetKeyDown(reloadButton)) {
            Cooldown();
            reloading = true;
            UpdateUI();
        }

        if (reloading) { // Reloading takes priority
            if(timer < reloadTime) {
                timer += Time.deltaTime;
            } else {
                Reload();
            }
        } else if (coolingDown) {
            if(timer < cooldownTime) {
                timer += Time.deltaTime;
            } else {
                Cooldown();
            }
        }
    }

    void UpdateUI() {
        if (reloading) {
            ammoCountText.text = "...";
        } else {
            ammoCountText.text = currentShots.ToString();
        }
    }

    void Cooldown() {
        coolingDown = false;
        timer = 0;
    }

    void Reload() {
        currentShots = maxAmmo;
        reloading = false;
        UpdateUI();
        timer = 0;
        reloadSound.Play();
    }


    void FireGun() {
        List<RaycastHit> hits = new List<RaycastHit>();
        Vector3 shootRay;
        shootSound.Play();
        coolingDown = true;
        currentShots -= 1;
        UpdateUI();
        Debug.Log(currentShots);

        for (int i = 0; i < pelletCount; i++) { // Loop through all of the projectiles
            shootRay = (playerCamera.forward + new Vector3(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread))) * range; // Modify each pellet by a slightly different amount each time

            RaycastHit hit;
            Physics.Raycast(bulletSpawnPoint.position, shootRay, out hit, range, ~playerLayer); // Fire a raycast from the center of the camera to the calculated destination, assign it to the premade hit variable, at max range, and ignore the player layer
            Debug.DrawRay(bulletSpawnPoint.position, shootRay, Color.green, 10f); // Draw the raycast in debug for my sake :)

            if(hit.transform == null) { // Ignore any misses (for the time being)
                continue;
            } else { // Print out the name of the object hit
                if (hit.transform.gameObject.layer == enemyLayer) { // Organize the hits to prioritize headshots first
                    if(hit.transform.gameObject.tag == "Head") {
                        hits.Insert(0, hit);
                    } else {
                        hits.Add(hit);
                    }
                }
            }
        }

        foreach (RaycastHit hit in hits) {
            hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
