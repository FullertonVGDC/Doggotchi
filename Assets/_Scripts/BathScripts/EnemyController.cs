using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float speed;
	public bool destroyOnDeath;

	private DogObject pc;
	private int pooHealth;
	private int maxPooHealth = 50;
	private Transform dogObj;

	public void Damage(int dmg){
		pooHealth -= dmg;

		if(pooHealth <= 0){
			if(destroyOnDeath){
				Destroy(gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
		pooHealth = maxPooHealth;
		GameObject playerController = GameObject.FindWithTag("doge");

		if(playerController != null){
			pc = playerController.GetComponent<DogObject>();
		}

		if (playerController == null){
			Debug.Log("Cannot find 'dogObject' script");
		}
		
		dogObj = GameObject.FindGameObjectWithTag("doge").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, dogObj.transform.position, speed * Time.deltaTime);

		if(pc.DidClick == true && pc != null){
			Damage(100);
		}
	}
}
