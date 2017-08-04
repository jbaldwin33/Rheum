using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Stats> ().health <= 0) {
			player.transform.position = transform.position;
			player.GetComponent<Stats> ().health = player.GetComponent<Stats> ().maxHealth;
		}
	}
}
