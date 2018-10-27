using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	/* Spawn rate & values variables */
	public GameObject[] enemies;
	public Vector3 spawnValues;
	public float spawnWait;
	public float spawnMaxWait;
	public float spawnMinWait;
	public int startWait;
	public bool stop;

	/* Score variables */
	public Text scoreText;
	private int score;


	int randSpawn;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore();
		StartCoroutine(Spawner());
	}
	
	// Update is called once per frame
	void Update () {
		spawnWait = Random.Range(spawnMinWait, spawnMaxWait);
	}

	IEnumerator Spawner(){
		yield return new WaitForSeconds(startWait);

		while(!stop){
			randSpawn = Random.Range(0, 2);

			Vector3 spawnPos = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);
			Instantiate(enemies[randSpawn], spawnPos + transform.TransformPoint(0,0,0), 
				gameObject.transform.rotation);

			yield return new WaitForSeconds(spawnWait);
		}
	}

	/* Current Mechanic State: Score system
	 * May change to a fill gauge, or back to health system */
	public void AddScore(int addScore_){
		score += addScore_;
		UpdateScore();
	}

	void UpdateScore(){
		scoreText.text = "S c o r e : " + score;
	}
}
