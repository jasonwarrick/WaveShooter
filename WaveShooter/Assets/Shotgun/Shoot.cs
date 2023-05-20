using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float cooldown;
    [SerializeField] float maxAmmo;

    public delegate void PlayerShoot();
    public static PlayerShoot playerShoot;
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            playerShoot?.Invoke();
            // Add force to player in the opposite direction of the camera
        }
    }
}
