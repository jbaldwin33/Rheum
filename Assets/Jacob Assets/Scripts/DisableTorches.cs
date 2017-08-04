using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DisableTorches : MonoBehaviour {

    public Transform torches;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        disableTorches();
    }

	private void OnTriggerEnter(Collider collision)
    {
        disableTorches();

    }

    private void disableTorches()
    {
        print("yar");
        for (int i = 0; i < torches.childCount; i++)
        {
            Transform trans = torches.GetChild(i);
            trans.Find("Particle System").GetComponent<Light>().enabled = false;
            //torches.GetChild(i).Find("Particle System").GetComponent<Light>().enabled = false;
        }
    }
}
