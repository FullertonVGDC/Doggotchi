using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogPlayer : MonoBehaviour  {
	[SerializeField]
	private StatSystem hygeine;
	private bool didClick;
	private ScoreManager score;

	public float scoreConv;
	public float finalMult;
	public Board board;

	private void Awake(){
		hygeine.Initialize();
	}

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board>();
		score = FindObjectOfType<ScoreManager>();
	}
	
	public void UpdateHygiene(int bonus) {
		if(score != null){
			scoreConv = score.points;
			finalMult = scoreConv / 100 + bonus;
			hygeine.CurrentValue += finalMult;
		}
	}
}
