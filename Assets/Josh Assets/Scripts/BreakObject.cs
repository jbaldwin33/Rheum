using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour {

    GameObject player;
    GameObject sword;
    public GameObject remains;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        sword = GameObject.Find("Sword");
	}
	
	// Update is called once per frame
	/*void Update () {
        if (Input.GetKeyDown("b"))
        {
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == sword)
        {
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
}
