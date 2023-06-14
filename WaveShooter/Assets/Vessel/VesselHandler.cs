using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselHandler : MonoBehaviour
{
    [SerializeField] float throwingForce;

    [SerializeField] List<GameObject> vessels;

    bool thrown = false;

    // Start is called before the first frame update
    void Start() {
        vessels[0].SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            ThrowVessel();
        }
    }

    void ThrowVessel() {
        vessels[0].SetActive(true);
        vessels[0].transform.parent = null;
        thrown = true;
        vessels[0].GetComponentInChildren<Rigidbody>().AddForce(transform.forward * throwingForce, ForceMode.Impulse);
    }
}
