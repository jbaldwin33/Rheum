using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Vector3 respawnOffset = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RespawnManager resp = other.GetComponent<RespawnManager>();
            resp.respawnPos = transform.position + respawnOffset;
        }
    }
}
