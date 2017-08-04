using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWaveStart : MonoBehaviour {

	public GameObject [] spiderSpawner;
	private bool hasBeenTriggered;
	private GameObject player;

	void Awake() {
		player = GameObject.Find ("Player");
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player") && !hasBeenTriggered) {
			foreach(GameObject spawner in spiderSpawner) {
				spawner.GetComponent<SpiderSpawner> ().startSpawning ();
			}
			hasBeenTriggered = true;
		}
	}

	void Update() {
		if (player.GetComponent<Stats>().health <= 0) {
			hasBeenTriggered = false;
		}
	}
}
