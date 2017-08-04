using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

	public GameObject text;
	private Rigidbody lever;
	private Transform leverTrans;
	private bool canPress;
	private bool leverOn;
	private float magnitude;


	void Start() {
		lever = GetComponentInChildren<Rigidbody> ();
		leverTrans = GetComponentInChildren<Transform> ();
		magnitude = -2.5f;
	}

	//changing our UI text so that the player knows how to activate the lever
	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
			text.SetActive (true);
			canPress = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals ("Player")) {
			text.SetActive (false);
			canPress = false;
		}
	}

	void FixedUpdate() {
		//if the player is within the area of the lever, they can activate it
		if (canPress && Input.GetKeyDown(KeyCode.F)) {
			print ("f");
			if (leverOn) {
				print ("ok");
				//deactivating the lever
				lever.velocity +=  leverTrans.forward * -magnitude;
			} else {
				//activating the lever
				lever.velocity += leverTrans.forward * magnitude;
			}
			leverOn = !leverOn;
		}
	}


}
