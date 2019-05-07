using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialZone : MonoBehaviour {

    public int addPoints;
    public int multiplyPoints;
    public ParticleSystem specialParticle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ball")
            specialParticle.Play();
    }
}
