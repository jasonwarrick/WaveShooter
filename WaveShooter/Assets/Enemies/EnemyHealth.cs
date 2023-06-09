using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] bool weakPoint; // Indicates if the given body part is a weak point
    bool killed;

    EnemyAI enemyAI;
    NavMeshAgent navmeshAgent;

    void Start() {
        enemyAI = transform.parent.GetComponent<EnemyAI>();
        navmeshAgent = transform.parent.GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float damage) {
        hp -= damage;
        // Debug.Log("Took " + damage + " damage");

        if (weakPoint) {
            Debug.Log("Headshot");
        }

        if (hp <= 0 & !killed) {
            if (weakPoint) {
                HeadKill();
            } else {
                BodyShot();
            }
        }
    }

    void HeadKill() {
        killed = true;
        enemyAI.enabled = false;
        navmeshAgent.enabled = false;
        gameObject.SetActive(false);
    }

    void BodyShot() {
        transform.parent.gameObject.SetActive(false);
    }
}
