using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {

	private bool open = false;
	private Vector3 openPosition;
	private Vector3 closedPosition;
	private AudioSource audio;

	void Awake() {
		closedPosition = transform.position;
		openPosition = transform.position - new Vector3 (0, 8f, 0);
		audio = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals("Player")) {
			audio.Stop ();
			open = true;
			audio.Play ();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.name.Equals("Player")) {
			audio.Stop ();
			open = false;
			audio.Play ();
		}
	}

	void FixedUpdate() {
		if (!open) {
			transform.position = Vector3.Lerp(transform.position, closedPosition, 2 * Time.deltaTime);

		} else {
			transform.position = Vector3.Lerp(transform.position, openPosition, 2 * Time.deltaTime);
		}
	}
}
