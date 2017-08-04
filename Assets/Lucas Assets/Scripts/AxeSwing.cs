using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("axe")) {
			GetComponent<AudioSource> ().Play ();
		}
	}
}
