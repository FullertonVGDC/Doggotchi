using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour {

	public float xSpeed;
	

	// Use this for initialization
	void Start () {
		xSpeed += Random.Range(-4f, 0);
		GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -12) {
			Destroy(gameObject);
		}
	}
}
