using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour {

    public ParticleSystem[] firework;
    public AudioSource fireworkSource;

    public ParticleSystem inPlay;
    public AudioSource coinPlus1Source;

    public ParticleSystem hitFrame;

    public void HomeRun()
    {
        int rand = Random.Range(0, firework.Length-1);
        firework[rand].Play();
        fireworkSource.Play();
    }

    public void InPlay()
    {
        inPlay.Play();
        coinPlus1Source.Play();
    }

    public void HitFrame()
    {
        hitFrame.Play();
    }

}
