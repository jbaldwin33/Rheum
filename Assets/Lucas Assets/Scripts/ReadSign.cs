using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSign : MonoBehaviour {

	private GameObject player;
	private static GameObject backdrop;
	public GameObject text;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		if (backdrop == null) {
			backdrop = GameObject.Find ("Backdrop");
		}
		backdrop.SetActive (false);
//		if (backdrop != null) {
//			backdrop.SetActive (false);
//		
//		}
	}
	
	// Update is called once per frame
//	void Update () {
//		float distance = Vector3.Distance (transform.position, player.transform.position);
//		distance = Mathf.Abs (distance);
//		if (distance < 2.0f) {
//			text.SetActive (true);
//			backdrop.SetActive (true);
//		} else {
//			text.SetActive (false);
//			backdrop.SetActive (false);
//		}
//	}

	void OnTriggerEnter(Collider other) {
		text.SetActive (true);
		backdrop.SetActive (true);
	}
	void OnTriggerExit(Collider other) {
		text.SetActive (false);
		backdrop.SetActive (false);
	}
}
