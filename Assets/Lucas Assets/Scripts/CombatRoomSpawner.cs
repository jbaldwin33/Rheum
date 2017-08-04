using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomSpawner : MonoBehaviour {

	public GameObject [] enemies;
	public Transform [] locations;

	private bool hasSpawned = false;

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player") && !hasSpawned) {
			print (true);
			hasSpawned = true;
			int[] minions = getEnemies ();
			Instantiate (enemies [minions[0]], locations [0].position, Quaternion.identity);
			Instantiate (enemies [minions[1]], locations [1].position, Quaternion.identity);
//			Instantiate (enemies [minions[2]], locations [2].position, Quaternion.identity);
		}
	}

	int [] getEnemies() {
		int loc1 = Random.Range (0, 2);
		int loc2 = Random.Range (0, 2);
		int loc3 = Random.Range (0, 2);
		return new int[3] {loc1, loc2, loc3};
	}
}
