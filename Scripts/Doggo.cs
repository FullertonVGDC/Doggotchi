using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour
{
	private Rigidbody2D rB;
	private Animator anim;

	//**Player Physics**
	private bool canMove = true;
	[SerializeField]
	private float movementSpeed = 4f;
	[SerializeField]
	private float jumpForce = 31f;
	private bool moveLeft;
	private bool moveRight;
	private bool jump;
	private bool sideFacing = true; //false is left true is right
	[SerializeField]
	private bool isGrounded;        //if character is on the ground

	//**Score**
	[SerializeField]
	public int boneCount = 0;

	// Use this for initialization
	void Start ()
	{
		rB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetInput();
		SwapSpriteSide();
	}
	private void FixedUpdate()
	{
		CheckIfGrounded();
	}
	private void GetInput()
	{
		if (Input.GetKey(KeyCode.A))
		{
			moveLeft = true;
		}
		else
		{
			moveLeft = false;
		}
		if (Input.GetKey(KeyCode.D))
		{
			moveRight = true;
		}
		else
		{
			moveRight = false;
		}
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			jump = true;
		}
			Move();
	}
	private void Move()
	{
		if (canMove)
		{
			if (moveLeft)
			{
				rB.velocity = new Vector2(-movementSpeed, rB.velocity.y);
				sideFacing = false;
				if (isGrounded)
				{
					anim.SetBool("isMoving", true);
				}
				else
				{
					anim.SetBool("isMoving", false);
				}
			}
			if (moveRight)
			{
				rB.velocity = new Vector2(movementSpeed, rB.velocity.y);
				sideFacing = true;
				if (isGrounded)
				{
					anim.SetBool("isMoving", true);
				}
				else
				{
					anim.SetBool("isMoving", false);
				}
			}
			if (jump)
			{
				rB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				jump = false;
			}
			else if (!moveRight && !moveLeft)
			{
				anim.SetBool("isMoving", false);
			}
		}
	}
	private void SwapSpriteSide()
	{
		if (sideFacing)
		{
			transform.localScale = new Vector2(1f, 1f);
		}
		else
		{
			transform.localScale = new Vector2(-1f, 1f);
		}
	}
	private void CheckIfGrounded()
	{
		isGrounded = false;
		RaycastHit2D[] hit;

		hit = Physics2D.RaycastAll(transform.position, Vector2.down, 2.0f);

		foreach (var hitted in hit)
		{
			//Making the raycast bypass player collision
			if (hitted.collider.gameObject == gameObject)
			{
				continue;
			}
			//Checking for gorund collision
			if (hitted.collider.gameObject.tag == "Ground")
			{
				isGrounded = true;
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bone")
		{
			++boneCount;
			collision.gameObject.SetActive(false);
		}
	}
}
