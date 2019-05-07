using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrePitch : MonoBehaviour {

    public Material[] pitchLights;
    //public AudioClip clip;

    AudioSource source;

    [Range(0.0f, 2.0f)]
    public float offSet;
    [Range(1.0f, 10.0f)]
    public float ratio;
    float greenTime;
    float redTime;
    float redStartTime;
    
    GameObject child;
    Material childMaterial;


    private void Start()
    {
        redTime = Game.intervalSpeed / ratio;
        greenTime = Game.intervalSpeed - redTime;
        redStartTime = redTime - offSet;
        
        source = GetComponent<AudioSource>();
        child = GameObject.Find("light bulb");
    }


    public void isPlaying()
    {
        StartRed();
    }

    public void StartRed()
    {
        source.Play();
        child.GetComponent<Renderer>().material = pitchLights[1];
        Invoke("Green", redStartTime);
    }
    
    public void Green()
    {
        if(Game.isPlaying)
        {
            source.Play();
            child.GetComponent<Renderer>().material = pitchLights[2];
            Invoke("EndCheck", offSet + .5f);
        }
        else
        {
            source.Play();
            child.GetComponent<Renderer>().material = pitchLights[0];
        }
    }

    public void Red()
    {
        if(Game.isPlaying)
        {
            source.Play();
            child.GetComponent<Renderer>().material = pitchLights[1];
            Invoke("Green", redTime);
        }
        else
        {
            source.Play();
            child.GetComponent<Renderer>().material = pitchLights[0];
        }

    }

    public void BallSpawned()
    {
        if (Game.isPlaying)
        {
            Invoke("Red", offSet);
        }
        else
        {
            source.Play();
            child.GetComponent<Renderer>().material = pitchLights[0];
        }
    }

    public void EndCheck()
    {
        if (!Game.isPlaying)
        {
            source.Play();
            child.GetComponent<Renderer>().material = pitchLights[0];
        }
    }
    
}
