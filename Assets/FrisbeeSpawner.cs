﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeSpawner : MonoBehaviour {

	public GameObject frisbeePrefab;
	public float minSpawnTimerLength;
	public float maxSpawnTimerLength;
	float spawnTimer;

	// Use this for initialization
	void Start () {
		spawnTimer = Random.Range(minSpawnTimerLength, maxSpawnTimerLength);
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0) {
			spawnTimer = Random.Range(minSpawnTimerLength, maxSpawnTimerLength);
			Instantiate(frisbeePrefab);
		}
	}
}
