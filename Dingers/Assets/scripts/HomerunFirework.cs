using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomerunFirework : MonoBehaviour {

	public ParticleSystem[] firework;
	public AudioSource fireworkSource;


	public ParticleSystem inPlay;
	public AudioSource coinPlus1Source;

    public ball myBall;

    public bool ballInPlay = false;

	// Use this for initialization
	void Start () {
        myBall = GetComponent<ball>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider trigger)
	{
		//+3 Play firework sound and particle system
		int rand = Random.Range (0, 3);
		if (trigger.gameObject.tag == "Homerun") {
			firework [rand].Play ();
			fireworkSource.Play ();
		}

        //+1 Play coin sound and particle system
        if (trigger.gameObject.tag == "Inplay" && !ballInPlay && !myBall.bs.foul) {
			inPlay.Play ();
            ballInPlay = true;
			coinPlus1Source.Play ();
		}
	}
    
}
