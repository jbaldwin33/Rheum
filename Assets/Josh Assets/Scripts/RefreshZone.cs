using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshZone : MonoBehaviour {

	public bool AddSleep;

    GameObject player;
    bool inside;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	

    void Update()
    {
        if (inside)
        {
            
			if (AddSleep) {
				player.GetComponent<Stats> ().addSleep (10f);
			} else {
				player.GetComponent<Stats> ().depleteSleep (10f);
			}

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inside = false;
        }
    }
}
