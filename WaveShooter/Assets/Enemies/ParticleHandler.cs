using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem headshotParticles;
    [SerializeField] ParticleSystem bodyshotParticles;

    bool particlesPlaying = false;
    bool headshotDone = false;

    void Update() {
        if (particlesPlaying && !headshotParticles.IsAlive()) {
            headshotDone = true;
            Debug.Log("headshot done");
        }
    }

    public void HeadshotParticles() {
        headshotParticles.Play();
        particlesPlaying = true;
    }
}
