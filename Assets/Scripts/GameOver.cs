using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	private Animator anim;
	[SerializeField]
	private TimerText tT;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (tT.timeLeftInt <= 1)
		{
			anim.SetBool("blackOut", true);
		}
		if (tT.timeLeftInt <= 0)
		{
			anim.SetBool("gameOver", true);
		}
	}
}
