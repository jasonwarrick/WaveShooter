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
    [SerializeField] float rayCount;

    public delegate void PlayerShoot();
    public static PlayerShoot playerShoot;
    
    // Start is called before the first frame update
    void Start() {
        playerShoot += FireGun;
        playerCamera = transform.parent.transform;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            playerShoot?.Invoke();
            // Add force to player in the opposite direction of the camera
        }
    }

    void FireGun() {
        Vector3 shootRay;

        for (int i = 0; i < rayCount; i++) { // Loop through all of the projectiles
            shootRay = (playerCamera.forward + new Vector3(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread))) * range; // Modify each pellet by a slightly different amount each time

            RaycastHit hit;
            Physics.Raycast(transform.position, shootRay, out hit);
            Debug.DrawRay(transform.position, shootRay, Color.green, 10f);
            if(hit.transform == null) {
                Debug.Log("Nada");
            } else {
                Debug.Log("Hit " + hit.transform.name);
            }
        }
    }
}
