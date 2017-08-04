using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour {

	private bool isBlocked;
	private float time;
	private float spawnTime;
	private bool waveInProgress;
	private GameObject player;

	public float minTime;
	public float maxTime;
	public GameObject spider;

	void Awake() {
		isBlocked = true;
		player = GameObject.Find ("Player");
	}

	void FixedUpdate() {

		//only spawn if the path is not blocked
		if (!isBlocked) {
			//create a new respawn timer
			if (!waveInProgress) {
				spawnTime = getNewSpawnTime ();
				waveInProgress = true;
			}

			time += Time.deltaTime;

			//spawn enemy
			if (time > spawnTime) {
				waveInProgress = false;
				Instantiate (spider, transform.position, Quaternion.identity);
				time = 0f;
			}
		}

		if (player.GetComponent<Stats> ().health <= 0) {
			stopSpawning();
		}
	}

	//get a random time to spawn an enemy
	float getNewSpawnTime() {
		return Random.Range (minTime, maxTime);
	}

	public void stopSpawning() {
		isBlocked = true;
	}

	public void startSpawning() {
		isBlocked = false;
	}
}
