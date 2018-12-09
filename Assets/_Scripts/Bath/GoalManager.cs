using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmptyGoal{
	public int numberNeeded;
	public int numberCollected;
	public Sprite goalSprite;
	public string matchValue;
}

public class GoalManager : MonoBehaviour {
	public EmptyGoal[] levelGoals;
	public List<GoalPanel> currentGoals = new List<GoalPanel>();
	public GameObject goalPrefab;
	public GameObject goalIntroParent;
	public GameObject goalGameParent;
	private EndGameManager endGame;

	// Use this for initialization
	void Start () {
		endGame = FindObjectOfType<EndGameManager>();
		InitGoal();
	}

	void InitGoal(){
		for(int i = 0; i < levelGoals.Length; ++i){
			GameObject goal = Instantiate(goalPrefab, goalGameParent.transform.position, Quaternion.identity);
			goal.transform.SetParent(goalGameParent.transform);

			GoalPanel panel = goal.GetComponent<GoalPanel>();
			currentGoals.Add(panel);
			panel.thisSprite = levelGoals[i].goalSprite;
			panel.thisString = "0 /" + levelGoals[i].numberNeeded;

		}
	}

	public void UpdateGoals(){
		int goalsCompleted = 0;
		for(int i = 0; i < levelGoals.Length; ++i){
				currentGoals[i].thisText.text = "" + levelGoals[i].numberCollected + "/" + levelGoals[i].numberNeeded;
				if(levelGoals[i].numberCollected >= levelGoals[i].numberNeeded){
					goalsCompleted++;
					currentGoals[i].thisText.text = "" + levelGoals[i].numberNeeded + "/" + levelGoals[i].numberNeeded;
				}
		}

		if(goalsCompleted >= levelGoals.Length){
			if(endGame != null){
				endGame.WinGame();
			}
		//	Debug.Log("You win!");
		}
	}

	public void CompareGoal(string s){
		for(int i = 0; i < levelGoals.Length; ++i){
			if(s == levelGoals[i].matchValue){
				levelGoals[i].numberCollected++;
			}
		}
	}
}
