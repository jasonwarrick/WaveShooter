using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp;

    public void TakeDamage(float damage) {
        hp -= damage;
        Debug.Log("Took " + damage + " damage");

        if (hp <= 0) {
            KillEnemy();
        }
    }

    void KillEnemy() {
        gameObject.SetActive(false);
    }
}
