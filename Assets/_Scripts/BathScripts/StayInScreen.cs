using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInScreen : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9f, 9f), 
			Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);
	}
}
