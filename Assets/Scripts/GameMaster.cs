using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	private GameObject player;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("Doggo");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	private void FixedUpdate()
	{
		FindIfFellOff();
	}
	private void FindIfFellOff()
	{
		if (player.transform.position.y < -10f)
		{
			GameOver();
		}
	}
	private void GameOver()
	{

	}
}
