using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float speed;
	public bool destroyOnDeath;
	public int scoreValue;

	private string currentWeapon;
	private GameController gc;
	private int pooHealth;
	private int maxPooHealth = 50;
	private Transform dogObj;

	// Use this for initialization
	void Start () {
		pooHealth = maxPooHealth;
		GameObject gameController = GameObject.FindWithTag("pooSpawn");

		if(gameController != null){
			gc = gameController.GetComponent<GameController>();
		}

		if (gameController == null){
			Debug.Log("Cannot find 'GameController' script");
		}
		currentWeapon = IsWeapon.weapon;
		dogObj = GameObject.FindGameObjectWithTag("doge").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, dogObj.transform.position, speed * Time.deltaTime);
	}

	public void Damage(int dmg){
		pooHealth -= dmg;

		if(pooHealth <= 0){
			if(destroyOnDeath){
				Destroy(gameObject);
				gc.AddScore(scoreValue);
			}
		}
	}

	/* Current State Mechanic: Click on Poos to kill them. May change! */
	void OnMouseOver(){
		if(Input.GetMouseButton(0) && currentWeapon == "brush"){
			Damage(10);
		}
	}
	

}
