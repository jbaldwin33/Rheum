using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingPlatform : MonoBehaviour {

	public float speed;
	public float timelimit;
	public float pause;
	public bool finalPlatform;


	private float time;
	private AudioSource sound;

	void Awake() {
		sound = GetComponent<AudioSource> ();
	}

	void FixedUpdate() {

		if (!finalPlatform) {
			Vector3 pos = transform.position;
			time += Time.deltaTime;

			if (time > timelimit) {
				sound.Stop ();
				if (time > (timelimit + pause)) {
					speed *= -1;
					time = 0f;
				}
			}  else {

				if (!sound.isPlaying) {
					sound.Play ();
				}
				transform.Translate (new Vector3(0, speed * Time.deltaTime, 0));
			}
		}
	}

	public void switchPlatform(bool val) {
		finalPlatform = val;
		gameObject.isStatic = false;
	}
}
