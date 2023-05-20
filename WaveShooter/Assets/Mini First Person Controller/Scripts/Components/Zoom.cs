using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera playerCam;
    FirstPersonMovement fpsScript;
    public float defaultFOV;
    public float sprintingFOV;
    public float lerpSpeed;

    void Awake() {
        // Get the camera on this gameObject and the defaultZoom.
        playerCam = GetComponent<Camera>();
        fpsScript = GetComponentInParent<FirstPersonMovement>();

        if (playerCam) {
            defaultFOV = playerCam.fieldOfView;
        }
    }

    void FixedUpdate() {
        if(fpsScript.IsRunning) {
            // If the player is sprinting, increase the FOV to the sprinting FOV
            if (playerCam.fieldOfView < sprintingFOV) { 
                playerCam.fieldOfView += lerpSpeed;
            }
        } else {
            if (playerCam.fieldOfView > defaultFOV) {
                playerCam.fieldOfView -= lerpSpeed;
            }
        }
    }
}
