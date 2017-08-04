using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour {

	private Rigidbody body;
	private AudioSource sound;
	Vector3 zero;

	void Awake() {
		body = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();
		zero = new Vector3 (0f, 0f, 0f);
	}

	void Update() {
		if (body.velocity != zero) {
			if (!sound.isPlaying) {
				sound.Play ();
			}
		} else {
			sound.Stop();
		}
	}
}
