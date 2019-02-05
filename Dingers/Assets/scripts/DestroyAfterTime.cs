using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	/*this script is used to delete the baseballs after a certain amount of time. It might not be needed 
	 *but it very well may be needed for optimization
	 */

	public float destroyTimer;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, destroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
