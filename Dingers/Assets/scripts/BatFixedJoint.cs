using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFixedJoint : MonoBehaviour {

    public Rigidbody rb;

    void Start()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.enablePreprocessing = false;
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = rb;
    }
}
