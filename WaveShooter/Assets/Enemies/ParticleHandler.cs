using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem headshotParticles;
    [SerializeField] ParticleSystem bodyshotParticles;

    [SerializeField] Transform headshotPoint;
    [SerializeField] Transform bodyshotPoint;

    float headshotWaitTime;
    float timer = 0f;

    bool particlesPlaying = false;
    bool headshotDone = false;

    void Start() {
        headshotWaitTime = headshotParticles.main.duration;
    }

    void Update() {
        if (particlesPlaying && timer < headshotWaitTime) {
            timer += Time.deltaTime;

            if (timer >= headshotWaitTime) {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void HeadshotParticles() {
        Instantiate(headshotParticles, headshotPoint.position, Quaternion.identity);
        particlesPlaying = true;
    }

    public void BodyshotParticles() {
        Instantiate(bodyshotParticles, bodyshotPoint.position, Quaternion.identity);
    }
}
