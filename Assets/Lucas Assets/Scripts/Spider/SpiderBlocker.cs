using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBlocker : MonoBehaviour {

	public GameObject spiderSpawner;
	private static int pathsBlocked;
	private GameObject centerBlock;
	private AudioSource sound;
	private bool played;

	void Awake() {
		centerBlock = GameObject.Find ("Center Block");
		sound = GetComponent<AudioSource> ();
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("block")) {
			spiderSpawner.GetComponent<SpiderSpawner> ().stopSpawning ();
			pathsBlocked++;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals("block")) {
			spiderSpawner.GetComponent<SpiderSpawner> ().startSpawning ();
			pathsBlocked--;
		}
	}

	void Update() {
		if (pathsBlocked == 3) {
			centerBlock.GetComponent<PushBack> ().setPushing (false);
			if (!played) {
				sound.Stop ();
				sound.Play ();
				played = true;
			}

		} else {
			if (centerBlock == null) {
				centerBlock = GameObject.Find ("Center Block");
			} else {
				centerBlock.GetComponent<PushBack> ().setPushing (true);
				played = false;
			}

		}
	}
}
