using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchEnabler : MonoBehaviour {

	public GameObject torches;


	void Awake() {
		torches.SetActive (false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player")) {
			torches.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.name.Equals("Player")) {
			torches.SetActive(false);
		}
	}
}
