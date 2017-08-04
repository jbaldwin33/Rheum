using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

    //Public vars
    public Vector3 respawnPos;
    public int respawnTime = 200;

    //Components
    Stats stats;

    //Private vars
    int respawnTimer = 0;

	// Use this for initialization
	void Start () {
        stats = GetComponent<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		if (respawnTimer > 0)
        {
            respawnTimer--;
        }
        if (respawnTimer == 0 && stats.health <= 0)
        {
            respawnTimer = respawnTime;
            stats.invincFrames = respawnTime * 2;
        }
        if (respawnTimer == respawnTime/2)
        {
            Game_Helper.screenFader.alpha = 1f;
        }
        if (respawnTimer == 1)
        {
            Game_Helper.player.transform.position = respawnPos;
            if (stats.health <= 0)
            {
                stats.health = stats.maxHealth;
            }
            Game_Helper.screenFader.alpha = 0f;

        }
	}

    public void respawn()
    {
        respawnTimer = respawnTime;
    }
}
