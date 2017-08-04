using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {
    public GameObject currentCube;
    private GameObject player;
    private float wait;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.Find("Cube1") || other.gameObject == player)
        {
            if (gameObject.name == "Switch1")
            {
                GameObject.Find("AOFWalls").GetComponent<WallScript>().openDoor1 = true;
            }
            else if (gameObject.name == "Switch2")
            {
                GameObject.Find("AOFWalls").GetComponent<WallScript>().openDoor2 = true;
            }
            GameObject.Find("EventListener").GetComponent<EventListenerScript>().onSwitch = true;
        }

        if (other.gameObject == player)
        {
            if (gameObject.name == "EnterRoomTrigger")
            {
                GameObject.Find("AOFWalls").GetComponent<WallScript>().ShutDoor();
                Destroy(gameObject);
            }
        }
        if (other.gameObject.name == "IllusionCube" || other.gameObject == player)
        {
            if (gameObject.name == "AreaThreeSwitch")
            {
                GameObject.Find("AOSWalls").GetComponent<WallScript>().openDoor3 = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.Find("Cube1") || other.gameObject.name == "IllusionCube" || other.gameObject == player)
        {
            if (gameObject.name == "Switch1")
            {
                GameObject.Find("AOFWalls").GetComponent<WallScript>().openDoor1 = false;
            }
            if (gameObject.name == "Switch2")
            {
                GameObject.Find("AOFWalls").GetComponent<WallScript>().openDoor2 = false;
            }
            if (gameObject.name == "AreaThreeSwitch")
            {
                GameObject.Find("AOSWalls").GetComponent<WallScript>().openDoor3 = false;
            }
            GameObject.Find("EventListener").GetComponent<EventListenerScript>().onSwitch = false;
        }
    }       
}
