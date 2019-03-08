using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundController : MonoBehaviour {

	public AudioSource hitBall;
	private float relativeVelocity;
	public AudioClip ground;
	public AudioClip wall;

	// Use this for initialization
	void Start () {
		hitBall = gameObject.AddComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision)
	{
        /*
		if (collision.gameObject.CompareTag ("Bat")) {
			hitBall.clip = bat;
			relativeVelocity = collision.impulse.magnitude;
			float volume = relativeVelocity / 20;
			hitBall.volume = volume;
			hitBall.Play ();
		}
        */

		if( collision.gameObject.CompareTag("walloutfield") || collision.gameObject.CompareTag("wallbackstop"))
			{
			hitBall.clip = wall;
				relativeVelocity = collision.impulse.magnitude;
			float volume = (-.45f + (relativeVelocity / 30));
				hitBall.volume = volume;
				hitBall.Play ();
			}

		if(collision.gameObject.CompareTag("ground"))
		{
			hitBall.clip = ground;
			relativeVelocity = collision.impulse.magnitude;
			float volume = (-.2f + (relativeVelocity / 20));
			hitBall.volume = volume;
			hitBall.Play ();
		}

	}
}
