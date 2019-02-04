using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBatLeft : MonoBehaviour {

	public Transform target;
	public float speed;
	private bool isPulled = false;

    // Use this for initialization
    void Start()
    {
        isPulled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isPulled && Vector3.Distance(target.position, transform.position) > .01f)
        {
            isBeingPulled();
        }
    }

    public void GetControllerPull()
    {
        isPulled = true;
    }

    public void GetControllerNotPull()
    {
        isPulled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rightController" || other.tag == "leftController")
        {
            Debug.Log("bat controller collision");
            isPulled = false;
        }
    }

    private void isBeingPulled()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step * 40);
    }



}