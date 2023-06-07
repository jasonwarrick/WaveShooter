using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Transform playerCamera;
    
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float cooldown;
    [SerializeField] float maxAmmo;
    [SerializeField] float maxSpread;
    [SerializeField] float pelletCount;

    [SerializeField] LayerMask playerLayer;
    int enemyLayer; // Layer var needs to be int to be read by raycast

    public delegate void PlayerShoot();
    public static PlayerShoot playerShoot;
    
    // Start is called before the first frame update
    void Start() {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerShoot += FireGun; // Add the FireGun method to the playerShoot event
        playerCamera = transform.parent.transform;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            playerShoot?.Invoke(); // Fire the playerShoot event to all delegates
        }
    }

    void FireGun() {
        Vector3 shootRay;

        for (int i = 0; i < pelletCount; i++) { // Loop through all of the projectiles
            shootRay = (playerCamera.forward + new Vector3(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread))) * range; // Modify each pellet by a slightly different amount each time

            RaycastHit hit;
            Physics.Raycast(transform.position, shootRay, out hit, range, ~playerLayer); // Fire a raycast from the center of the camera to the calculated destination, assign it to the premade hit variable, at max range, and ignore the player layer
            Debug.DrawRay(transform.position, shootRay, Color.green, 10f); // Draw the raycast in debug for my sake :)

            if(hit.transform == null) { // Ignore any misses (for the time being)
                continue;
            } else { // Print out the name of the object hit
                if (hit.transform.gameObject.layer == enemyLayer) {
                    hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
                }

                Debug.Log("Hit " + hit.transform.gameObject.layer);
            }
        }
    }
}
