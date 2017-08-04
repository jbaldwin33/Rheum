using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {

	private Rigidbody rb;
	private float timePassed;
	private float shakeTime = 5.0f;
	private bool hasShook;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision collision) {
		if (!hasShook) {
			hasShook = true;
			StartCoroutine (shake());
		}
	}
	


	IEnumerator shake() {
		float time = 0.0f;
		while (time < shakeTime) {
			time += Time.deltaTime;
			Vector3 pos = transform.position;
			transform.position = new Vector3 (Time.deltaTime * getRandom() + pos.x, pos.y, Time.deltaTime * getRandom() + pos.z);
			yield return null;
		}
		rb.isKinematic = false;
		rb.useGravity = true;
		yield return null;
	}

	float getRandom() {
		return Random.Range (-0.5f, 0.5f);
	}
}
