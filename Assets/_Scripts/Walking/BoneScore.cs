using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoneScore : MonoBehaviour
{
	private Text scoreText;
	Doggo dog;
	// Use this for initialization
	void Start ()
	{
		dog = GameObject.Find("Doggo").GetComponent<Doggo>();
		scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = "Score: " + dog.boneCount;
	}
}
