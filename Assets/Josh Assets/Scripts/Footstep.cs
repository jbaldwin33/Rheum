using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {

    GameObject leftFoot;
    GameObject rightFoot;
    GameObject player;
    public bool leftNotGrounded = false;
    public bool rightNotGrounded = false;
	// Use this for initialization
	void Start () {
        leftFoot = GameObject.Find("CubeLeft");
        rightFoot = GameObject.Find("CubeRight");
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        
        
        /*if (player.GetComponent<HumanoidController>().grounded)
        {
            if (IsGrounded(leftFoot) && !leftNotGrounded)
            {
                GameObject.Find("EventListener").GetComponent<EventListenerScript>().leftDown = true;
                leftNotGrounded = true;
            }
            else
            {
                leftNotGrounded = false;
            }
            if (IsGrounded(rightFoot) && !rightNotGrounded)
            {
                GameObject.Find("EventListener").GetComponent<EventListenerScript>().rightDown = true;
                rightNotGrounded = true;
            }
            else
            {
                rightNotGrounded = false;
            }
        }*/
	}

    bool IsGrounded(GameObject cube)
    {
        return cube.transform.position.y > 0.25;
    }

    public void LeftDown()
    {
        GameObject.Find("EventListener").GetComponent<EventListenerScript>().leftDown = true;
    }
    public void RightDown()
    {
        GameObject.Find("EventListener").GetComponent<EventListenerScript>().rightDown = true;
    }
}
