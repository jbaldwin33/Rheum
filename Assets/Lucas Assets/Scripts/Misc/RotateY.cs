using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour {

	public float speed;
	public float direction;

	void FixedUpdate() {
		transform.Rotate (0, speed * direction * Time.deltaTime, 0);
	}
}
