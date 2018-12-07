using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EndGameReqs{
	public int counter;
}

public class EndGameManager : MonoBehaviour {
	public GameObject timeLabel;
	public EndGameReqs requirements;
	public Text counter;
	public int currentCounterVal;
	public GameObject youWin;
	public GameObject youLose;
	public GameObject scoreText;
	private float timer;
	private Board board;

	/* private void Awake(){
		hygeine.Initialize();
	} */

	/* 
		HEALTH STAT IMPLEMENTATION:
		failing game will decrease happiness not hygeine;
		SCORE BETWEEN A CERTAIN NUMBER:
			0 - 1000 : 25% of missing health restored
			1001 - 2000 : 50% of missing health restored
			2001 < : 100% of missing health restored
			If game objectives completed, they add to the score
	*/

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board>();
		InitGame();
	}

	void InitGame(){
		timer = 1;
		currentCounterVal = requirements.counter;
		counter.text = "" + currentCounterVal;
	}

	public void DecreaseTimer(){
		if(board.current != GameState.pause){
			currentCounterVal--;
			counter.text = "" + currentCounterVal;

			if(currentCounterVal <= 0 ){
				LoseGame();
			}
		}
	}

	public void WinGame(){
		youWin.SetActive(true);
		// scoreText.SetActive(true);
		board.current = GameState.win;
		currentCounterVal = 0;
		counter.text = "" + currentCounterVal;
		PanelOverlay fade = FindObjectOfType<PanelOverlay>();
		fade.GameOver();

	}

	public void LoseGame(){
		youLose.SetActive(true);
		// scoreText.SetActive(true);
		board.current = GameState.lose;
		Debug.Log("Timer done.");
		currentCounterVal = 0;
		counter.text = "" + currentCounterVal;
		PanelOverlay fade = FindObjectOfType<PanelOverlay>();
		fade.GameOver();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentCounterVal > 0){
			timer -= Time.deltaTime;
			if(timer <= 0 ){
				DecreaseTimer();
				timer = 1;
			}
		}
	}
}
