using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoleSpawner : MonoBehaviour {

	public Transform[] spawnLocations;
	public GameObject[] enemies;
	private GameObject enemy1;
	private GameObject enemy2;

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player")) {
			spawnEnemies ();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.name.Equals("Player")) {
			Destroy (enemy1);
			Destroy (enemy2);
		}
	}

	void spawnEnemies() {
		int[] locations = getLocations ();
		int[] minions = getEnemies ();
		enemy1 = Instantiate (enemies [minions [0]], spawnLocations [locations [0]].position, Quaternion.identity);
		enemy2 = Instantiate (enemies [minions [1]], spawnLocations [locations [1]].position, Quaternion.identity);
//		Instantiate (enemies [minions [2]], spawnLocations [locations [2]]);
	}

	int [] getLocations() {
		bool isSame = true;
		while (isSame) {
			int loc1 = Random.Range (0, 5);
			int loc2 = Random.Range (0, 5);
			int loc3 = Random.Range (0, 5);
			if (loc1 != loc2 && loc1 != loc3 && loc2 != loc3) {
				isSame = false;
				return new int[3] {loc1, loc2, loc3};
			}
		}
		return new int[3] { 1, 2, 3 };
	}

	int [] getEnemies() {
		int loc1 = Random.Range (0, 2);
		int loc2 = Random.Range (0, 2);
		int loc3 = Random.Range (0, 2);
		return new int[3] {loc1, loc2, loc3};
	}
}
