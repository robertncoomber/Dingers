using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControllerTests : MonoBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.enablePreprocessing = false;
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = rb;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
