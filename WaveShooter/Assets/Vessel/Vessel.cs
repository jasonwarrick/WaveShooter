using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour
{
    GameObject parent;
    int playerLayer;

    public int listOrder;
    
    // Start is called before the first frame update
    void Awake() { // Awake will run even if the object is not active/enabled
        parent = transform.parent.gameObject;
        playerLayer = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer == playerLayer) {
            Pickup();
        }
    }

    void Pickup() {
        transform.parent = parent.transform;
        gameObject.SetActive(false);
    }
}
