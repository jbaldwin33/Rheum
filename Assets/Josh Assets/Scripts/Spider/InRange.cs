using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : MonoBehaviour {

    public bool playerInRange;
    GameObject player;

    void Awake()
    {
        playerInRange = false;
        player = GameObject.Find("Player");

    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
