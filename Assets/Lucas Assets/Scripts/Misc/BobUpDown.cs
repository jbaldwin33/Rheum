using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUpDown : MonoBehaviour {

	//ideal speed = 0.05
	public float speed;
	//ideal time = 1
	private float time;

	void FixedUpdate() {
		Vector3 pos = transform.position;
		time += Time.deltaTime;

		if (time > 1f) {
			speed *= -1;
			time = 0f;
		} 

//		transform.position = new Vector3 (pos.x, (pos.y + speed) * Time.deltaTime, pos.z);
		transform.Translate (new Vector3(0, speed * Time.deltaTime, 0));
	}
}
