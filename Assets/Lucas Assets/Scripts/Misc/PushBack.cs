using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour {

	private GameObject origin;
	private Vector3 vec;
	private bool willPush;

	void Start() {
		origin = GameObject.Find ("ForceLocation");
		willPush = true;
		if (origin == null) {
			print ("Can't find Game origin");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (willPush && (other.name.Equals("Player") || other.tag.Equals("block"))) {
			print ("Pushed back");
			vec = other.transform.position - origin.transform.position;
//			other.GetComponent<Rigidbody> ().AddForce (vec * 50);
			if (other.tag.Equals("block")) {
				other.GetComponent<Rigidbody>().velocity = vec * 4;
			} else {
				other.GetComponent<Rigidbody>().velocity = vec * 2;
			}
		}

	}

	public void setPushing(bool val) {
		willPush = val;
	}
}
