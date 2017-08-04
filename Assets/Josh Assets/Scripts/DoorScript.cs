using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {

    GameObject player;
    public GameObject text;
    bool openDoor;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update() {

        Open();
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < 1.5f)
        {
            text.GetComponent<Text>().text = "Press 'n' to open";
            text.SetActive(true);

            if (Input.GetKeyDown("n"))
            {
                if (gameObject.name == "SlideDoor2")
                {
                    if (player.GetComponent<HumanoidController>().keys == 2)
                    {
                        openDoor = true;
                    }
                    else
                    {
                        text.GetComponent<Text>().text = "Not enough keys";
                    }
                }
                else if (gameObject.name == "SlideDoor3")
                {
                    if (GameObject.Find("EventListener").GetComponent<EventListenerScript>().bossSwitch)
                    {
                        openDoor = true;
                    }
                }
                else
                {
                    openDoor = true;
                }
            }
        }
        else
        {
            text.SetActive(false);
        }
    }

    void Open()
    {
        if (openDoor)
        {
            if (transform.position.y < 3.3f)
            {
                transform.Translate(new Vector3(0, 0.1f, 0));
            }
        }
    }
}