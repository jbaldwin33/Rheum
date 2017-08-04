using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFloorController : MonoBehaviour {

	public enum HoleState {
		ONE, TWO, THREE, FOUR, FIVE
	}

	public HoleState state;

	private Renderer rend;
	private float sleep;
	private float maxSleep;
	private GameObject player;

	void Awake() {
		rend = GetComponent<Renderer> ();
		player = GameObject.Find ("Player");
		sleep = player.GetComponent<Stats> ().sleep;
		maxSleep = player.GetComponent<Stats> ().maxSleep;
	}

	void Update() {
		// get amount of sleep player has in range of 0 to 100;
		sleep = (player.GetComponent<Stats> ().sleep / maxSleep) * 100;

		switch(state) {
		case HoleState.ONE:
			if (isVisible (0, 20)) {
				rend.enabled = false;
			} else {
				rend.enabled = true;
			}
			break;

		case HoleState.TWO:
			if (isVisible (20, 40)) {
				rend.enabled = false;
			} else {
				rend.enabled = true;
			}
			break;

		case HoleState.THREE:
			if (isVisible (40, 60)) {
				rend.enabled = false;
			} else {
				rend.enabled = true;
			}
			break;

		case HoleState.FOUR:
			if (isVisible (60, 80)) {
				rend.enabled = false;
			} else {
				rend.enabled = true;
			}
			break;

		case HoleState.FIVE:
			if (isVisible (0, 20) || isVisible(60, 80)) {
				rend.enabled = false;
			} else {
				rend.enabled = true;
			}
			break;
		}
	}


	bool isVisible(float min, float max) {
		if (sleep >= min && sleep < max) {
			return true;
		}
		return false;
	}

	void OnJointBreak(float breakForce) {
		GetComponent<AudioSource> ().Play ();
	}
}
