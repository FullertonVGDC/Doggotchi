using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	Transform player;
	Vector3 offset = new Vector3(0f, 0f, -10f);
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		transform.position = new Vector3(player.position.x, 0f) + offset;
	}
}
