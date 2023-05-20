using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    /**
    
    Basic FPS controller code taken from: https://www.youtube.com/watch?v=f473C43s8nE
    FIRST PERSON MOVEMENT in 10 MINUTES - Unity Tutorial by Dave / GameDevelopment
    
    **/

    [SerializeField] float sensX;
    [SerializeField] float sensy;

    [SerializeField] Transform orientation;

    float xRotation;
    float yRotation;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update () {
        HandleMouseInput();
    }

    void HandleMouseInput() {
        // Retrieve mouse rotation data
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensy;

        // Add the data to the current rotation
        yRotation += mouseX;

        xRotation += -mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
