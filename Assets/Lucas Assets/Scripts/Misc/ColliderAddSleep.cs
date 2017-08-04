using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderAddSleep : MonoBehaviour {

	//400f is 20%
	private GameObject sleepSlider;
	private static bool hasFallen;
	public GameObject spawner;
	private GameObject player;

	void Awake() {
		sleepSlider = GameObject.Find("SleepBar");
		player = GameObject.Find ("Player");
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player") && !hasFallen) {
			other.GetComponent<Stats> ().addSleep (400f);
			sleepSlider.GetComponent<BarScript>().currentSleep = other.GetComponent<Stats> ().sleep;
			hasFallen = true;
			StartCoroutine (respawn ());
		}

	}

	IEnumerator respawn() {
		yield return new WaitForSeconds (3.0f);
		player.transform.position = spawner.transform.position;
		hasFallen = false;
	}
}
