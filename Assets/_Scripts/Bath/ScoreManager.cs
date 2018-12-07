using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public int score;
	public int points;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//scoreText.text = score.ToString();
		string[] tmp = scoreText.text.Split(':');
		scoreText.text = tmp[0] + ": " + score;
	}

	public void IncreaseScore(int amountToIncrease){
		points = amountToIncrease;
		score += amountToIncrease;
	}
}
