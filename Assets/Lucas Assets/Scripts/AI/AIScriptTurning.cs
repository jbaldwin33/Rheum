using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScriptTurning : MonoBehaviour {

	public float speed;
	public float timeLimit;
	public Vector3 direction;

	//position
	private Vector3 pos;
	private float time;
	private bool rotate;
	private Quaternion targetAngle;

	//the degree the object should be facing after turning
	private float nextDegree;

	void Start() {
		
		if (speed == 0) {
			speed = 1;
		}

		nextDegree = 180f;

	}

	void FixedUpdate() {

			time += Time.deltaTime;

		if (time > timeLimit) {
			//only change the direction if the character is not rotating
//			direction *= -1f;
			if (rotate) {
				if (!transform.rotation.y.Equals(nextDegree)) {
					transform.rotation = Quaternion.Euler (0, nextDegree, 0);
					getNextDegree ();
				}
				time = 0f;
			} else {
				time = 3f;
			}
			rotate = !rotate;
			targetAngle = Quaternion.LookRotation (-transform.forward, Vector3.up);

		}

		//if we are not rotating, then we are translating
		if (rotate) {
			transform.rotation = Quaternion.Slerp (transform.rotation, targetAngle, 3f * Time.deltaTime);
		} else {
			transform.Translate (speed * direction * Time.deltaTime);
		}



	}

	//get our nextDegree that the play should face after turning
	void getNextDegree() {
		if (nextDegree == 180f) {
			nextDegree = 0f;
		} else {
			nextDegree = 180f;
		}
	}

//	IEnumerator rotate() {
//		transform.Rotate (0, 180, 0);
//		yield return new WaitForSeconds(2.0f);
//	}

//	void rotate() {
//		transform.Rotate (0, 180, 0);
//	}
}
