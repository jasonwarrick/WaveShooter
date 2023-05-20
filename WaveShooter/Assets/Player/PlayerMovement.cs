using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
    
    Basic FPS controller code taken from: https://www.youtube.com/watch?v=f473C43s8nE
    FIRST PERSON MOVEMENT in 10 MINUTES - Unity Tutorial by Dave / GameDevelopment
    
    **/

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float flatSpeedMultiplier;
    bool isSprinting = false;
    
    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("GroundCheck")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    public bool grounded;

    [SerializeField] Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        GetInput();
        SpeedControl();
        HandleDrag();
    }

    void HandleDrag() {
        if(grounded && horizontalInput == 0 && verticalInput == 0) {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        } else {
            rb.drag = 0;
        }
    }

    void FixedUpdate() {
        MovePlayer();
    }

    void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Check if jump is possible
        if(Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown); // Delay the resetting of jump
        }

        if(Input.GetKey(sprintKey) && grounded) {
            isSprinting = true;
        } else {
            isSprinting = false;
        }
    }

    void MovePlayer () {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded) {
            if(isSprinting) {
                rb.AddForce(moveDirection * sprintSpeed * flatSpeedMultiplier, ForceMode.Force);
            } else {
                rb.AddForce(moveDirection * moveSpeed * flatSpeedMultiplier, ForceMode.Force);
            }            
        } else if (!grounded) {
            rb.AddForce(moveDirection * moveSpeed * flatSpeedMultiplier * airMultiplier, ForceMode.Force);
        }
    }

    void SpeedControl() {
        // Get velocity without vertical
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float topSpeed;

        if(isSprinting) {
            topSpeed = sprintSpeed;
        } else {
            topSpeed = moveSpeed;
        }

        if (flatVel.magnitude > topSpeed) {
            // Reset the flat velocity to be maxed out at the move speed
            Vector3 limitedVel = flatVel.normalized * topSpeed;

            // Reapply this velocity to the player, while maintaining the original y velocity
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump() {
        // Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump () {
        readyToJump = true;
    }
}
