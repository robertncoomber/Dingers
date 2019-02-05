using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstantiator : MonoBehaviour {

    public Rigidbody ball;
	
	// Update is called once per frame
	void Update () {
        Rigidbody clone;
        clone = Instantiate(ball, transform.position, transform.rotation);
	}
}
