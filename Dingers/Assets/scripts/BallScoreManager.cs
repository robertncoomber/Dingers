using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoreManager : MonoBehaviour {

	private bool contactMade;
	private bool hitInPlay;
	private bool homerun;

	// Use this for initialization
	void Start () {
		contactMade = false;
		hitInPlay = false;
		homerun = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//everything that can come in contact with the ball
	//if it collides with the object of "___" it will
	//do "______".
	void OnCollisionEnter( Collision other )
	{

		//collision with the bad
		if (other.gameObject.tag == "bat") {
			contactMade = true;
		}

		//be sure to correct for ground rule double
		if (other.gameObject.tag == "double") {
			if (scoreManager.scoreNum < 0) {
				scoreManager.scoreNum = 0;
			} else {
				scoreManager.scoreNum = scoreManager.scoreNum * 2;
			}

			Destroy (this);
		}
	}

	//any trigger that the can go thru if it goes
	// thru with the object of "___" it will do "______".
	void OnTriggerEnter(Collider trigger)
	{
		//homerun
		if (trigger.gameObject.tag == "homerun") {
			if (hitInPlay == false) {
				scoreManager.scoreNum = scoreManager.scoreNum + 3;
			} else {
				scoreManager.scoreNum = scoreManager.scoreNum + 2;
			}

			Destroy (this);
		}

		//strike
		if (trigger.gameObject.tag == "strikezone") {
			if (contactMade != true) {
				scoreManager.scoreNum = scoreManager.scoreNum - 1;
				Debug.Log ("strikezone");
				Destroy (this);
			} 
			else {
				Destroy (this);
			}
		}


		//inplay
		if (trigger.gameObject.tag == "inplay") {
			if (contactMade == true && !homerun) {
				scoreManager.scoreNum = scoreManager.scoreNum + 1;
				Destroy (this);
				Invoke("makeInPlay", 3);
				Invoke ("DestroyThis", 6);
			}
		}

	}


	void makeInPlay()
	{
		hitInPlay = true;
	}

	void DestroyThis()
	{
		Destroy (this);
	}
}
