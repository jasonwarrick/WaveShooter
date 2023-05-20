using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform player;
    Rigidbody rb;
    GroundCheck groundCheck;
    
    // Start is called before the first frame update
    void Start() {
        rb = player.GetComponent<Rigidbody>();
        groundCheck = player.GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0) && !groundCheck.isGrounded) {
            Debug.Log("Knockback");
            // Add force to player in the opposite direction of the camera
        }
    }
}
