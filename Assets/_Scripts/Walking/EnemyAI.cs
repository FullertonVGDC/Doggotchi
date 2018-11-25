using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public LayerMask enemyMask;
	public float speed = 1f;
	Rigidbody2D rb;
	Transform myTransform;
	float myWidth;
	// Use this for initialization
	void Start ()
	{
		myTransform = this.transform;
		rb = GetComponent<Rigidbody2D>();
		myWidth = GetComponent<SpriteRenderer>().bounds.extents.x;	//gives width of sprite

	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		Vector2 lineCastPos = myTransform.position - myTransform.right * myWidth;
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
		bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

		//if there is no ground, turn around
		if (!isGrounded)
		{
			Vector3 currRot = myTransform.eulerAngles;
			currRot.y += 180;
			myTransform.eulerAngles = currRot;
		}

		//Always move forward
		Vector2 myVel = rb.velocity;
		myVel.x = -myTransform.right.x * speed;
		rb.velocity = myVel;
	}
	void Update ()
	{
		
	}
}
