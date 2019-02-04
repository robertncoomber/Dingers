using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxloadlevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter( Collision other )
	{

		if (other.gameObject.tag == "bat") {
			Debug.Log ("Enter Bat Trigger");
			Debug.Log ("Should Load");
			SteamVR_LoadLevel.Begin ("newField", true, 2.9f, 155f, 155f, 155f, 0.55f);
		}
		//if( other.gameObject.tag == "ball" )
		//scoreManager.scoreNum = scoreManager.scoreNum + 1;

	//void OnTriggerEnter(Colli
	}
}
