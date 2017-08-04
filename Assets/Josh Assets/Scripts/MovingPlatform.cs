using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    GameObject player;
    GameObject platformBack;
    float up = 0;
    float back = 0;
    float down = 0;
    bool left = true;
    bool right = false;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        platformBack = GameObject.Find("SequenceSwitch1back");
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("EventListener").GetComponent<EventListenerScript>().movePlatform)
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
            transform.localScale = new Vector3(0.4f, 0, 0.4f);
            platformBack.transform.localScale = new Vector3(0.4f, 0, 0.4f);
            Destroy(GetComponent<SequenceScript>());
            Move();
        }
        
	}

    void Move()
    {
        Debug.Log(Mathf.Round(down));
        if (Mathf.Round(up) != 15.0f)
        {
            transform.Translate(new Vector3(0, 0.05f, 0));
            platformBack.transform.Translate(new Vector3(0, -0.05f, 0));
            up += 0.05f;
        }
        /*else if (Mathf.Round(back) != 10.0f)
        {
            if (left)
            {
                transform.Translate(new Vector3(0, 0, -0.05f));
                platformBack.transform.Translate(new Vector3(0, 0, 0.05f));
            }
            else if (right)
            {
                transform.Translate(new Vector3(0, 0, 0.05f));
                platformBack.transform.Translate(new Vector3(0, 0, -0.05f));
            }
            back += 0.05f;
        }*/
        else if (Mathf.Round(down) != 15.0f)
        {
            transform.Translate(new Vector3(0, -0.05f, 0));
            platformBack.transform.Translate(new Vector3(0, 0.05f, 0));
            down += 0.05f;
        }
        else
        {
            up = 0;
            back = 0;
            down = 0;
            left = !left;
            right = !right;
        }
    }
}
