using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firebaseball : MonoBehaviour {

	public Rigidbody baseball;
	public float speed;
	public float interval = 2.0f;
	private float timer;
	public int startTime = 3;
	private bool isPlaying = false;

	void Start()
	{
		isPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {

		//Previous method of timing and firing baseballs
		/*
		timer += Time.deltaTime;
		if (timer >= interval) {
			Rigidbody instantiatedBaseball = Instantiate (baseball, transform.position, transform.rotation) as Rigidbody;
			instantiatedBaseball.velocity = transform.TransformDirection (new Vector3 (0, 0, speed));
			timer = 0;
		}
		*/

		if (isPlaying) {
			timer += Time.deltaTime;
		}

		if (timer > 120) {
			stopInvoke ();
		}


	}

	void throwBaseball()
	{
		Rigidbody instantiatedBaseball = Instantiate (baseball, transform.position, transform.rotation) as Rigidbody;
		instantiatedBaseball.velocity = transform.TransformDirection (new Vector3 (0, 0, speed));

	}
		
	public void startInvoke()
	//this function is used to start the invoke seperate from the start, if someone wants to pause and start it
	{
		//begin invoke of throw baseball
		InvokeRepeating("throwBaseball", startTime , interval);
		isPlaying = true;
	}

	public void stopInvoke()
	//this function is used to run cancel Invoke once when the game is stopped
	{
		CancelInvoke ("throwBaseball");
		isPlaying = false;

	}

	public void restartTimer()
	{
		timer = 0;
		isPlaying = true;
		startInvoke ();
	}


}
