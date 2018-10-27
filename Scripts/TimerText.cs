using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
	private Text timer;
	private float totalTime = 180f;
	private float timeLeft;
	public int timeLeftInt;
	// Use this for initialization
	void Start ()
	{
		timer = GetComponent<Text>();
		timeLeft = totalTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeLeft -= Time.deltaTime;
		timeLeftInt = (int)timeLeft;
		timer.text = "Timer: " + timeLeftInt;
	}
}
