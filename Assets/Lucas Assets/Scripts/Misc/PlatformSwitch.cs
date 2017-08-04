using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.transform.name.Equals("Player")) {
			GameObject.Find ("Center Block").GetComponent<RaisingPlatform> ().switchPlatform (false);
		}
	}
}
