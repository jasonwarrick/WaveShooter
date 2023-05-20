using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;

    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    [SerializeField] float knockbackForce;

    Rigidbody rb;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake() {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
        Shoot.playerShoot += ShootKnockback;
    }

    void FixedUpdate() {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0) {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
    }

    void ShootKnockback() {
        // Debug.Log("Knockback");
        // Vector3 forRay = -playerCamera.forward * 10;
        // Debug.DrawRay(transform.position, forRay, Color.green, 10f);
        // rb.AddForce(-playerCamera.forward * 100 * knockbackForce, ForceMode.VelocityChange);
        Debug.Log(-playerCamera.forward);
        rb.velocity += -playerCamera.forward * knockbackForce;
    }
}