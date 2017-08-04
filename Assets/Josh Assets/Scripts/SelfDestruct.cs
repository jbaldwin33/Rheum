using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	void Start()
    {
        StartCoroutine(WaitToDestroy());
    }

	IEnumerator WaitToDestroy () {

        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
	}
	
}
