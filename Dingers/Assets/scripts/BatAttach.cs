using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttach : MonoBehaviour {

    private FixedJoint Joint = null;
    public Rigidbody Bat;

    private void Awake()
    {
        Joint = GetComponent<FixedJoint>();
        Joint.connectedBody = Bat;
    }
}
