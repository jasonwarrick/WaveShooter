using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselHandler : MonoBehaviour
{
    [SerializeField] float throwingForce;

    [SerializeField] List<GameObject> vessels;
    [SerializeField] Transform playerCamera;

    bool empty = false;

    // Start is called before the first frame update
    void Start() {
        foreach (GameObject vessel in vessels) {
            vessel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            ThrowVessel();
        }
    }

    void ThrowVessel() {
        foreach (GameObject vessel in vessels) {
            if (vessel.activeInHierarchy) { // Vessels are only active when thrown, so if they're active, move onto the next one
                continue;
            } else {
                vessel.SetActive(true);
                vessel.transform.parent = null;
                vessel.GetComponentInChildren<Rigidbody>().AddForce(playerCamera.transform.forward * throwingForce, ForceMode.Impulse);
                return;
            }
        }

        empty = true;
    }
}
