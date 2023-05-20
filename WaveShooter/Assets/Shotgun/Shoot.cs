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
        Vector3 forRay;

        for (int i = 0; i < rayCount; i++) { // Loop through all of the projectiles
            forRay = (playerCamera.forward + new Vector3(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread))) * range; // Modify each pellet by a slightly different amount each time
            Debug.DrawRay(transform.position, forRay, Color.green, 10f);
        }
    }
}
