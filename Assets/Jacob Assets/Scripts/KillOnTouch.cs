using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Transform trans = collision.gameObject.transform;
        Stats stats = trans.GetComponent<Stats>();
        if (stats)
        {
            stats.takeDamage(stats.health);
        }
    }
}
