using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerWarning : MonoBehaviour
{
	private Image warning;
	[SerializeField]
	private TimerText time;
	private Animator anim;
	// Use this for initialization
	void Start ()
	{
		warning = GetComponent<Image>();
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate ()
	{
		if (time.timeLeftInt == 30)
		{
			anim.SetBool("warning", true);
		}
		else
		{
			anim.SetBool("warning", false);
		}
	}
}
