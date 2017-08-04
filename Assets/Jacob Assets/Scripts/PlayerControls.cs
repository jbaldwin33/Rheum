using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    //Public variables
    //public GameObject player;

    //Private variables
    HumanoidController controller;
    private int attackFrames;
    private GameObject player;

    //Used for instantiation
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<HumanoidController>();
    }
    /*
    private void Update()
    {

    }
    */
    // Update is called once per frame
    void Update () {
        if (attackFrames > 0)
        {
            attackFrames--;
        }
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        float horiz1 = Input.GetAxisRaw("Horizontal1");
        float vert1 = Input.GetAxisRaw("Horizontal1");
        bool jump = Input.GetButtonDown("Jump");
        bool attack = Input.GetButton("Attack");
        bool attackDown = Input.GetButtonDown("Attack");
        bool attackUp = Input.GetButtonUp("Attack");
        bool action = Input.GetButtonDown("Action");
        bool block = Input.GetButtonDown("Block");
        bool blockUp = Input.GetButtonUp("Block");
        bool dodge = Input.GetButtonDown("Dodge");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 movement = (forward * vert + right * horiz);

        
        
        if (movement.magnitude < 1f)
        {
            controller.throttle = movement.magnitude;
        } else
        {
            controller.throttle = 1f;
        }
        controller.inputDir = movement.normalized;
        controller.lookRot = transform.rotation;
        if (jump)
        {
            controller.jump();
        }
        if (attackDown)
        {
            attackFrames = 7;
        }
        if (attackUp)
        {
            controller.attack();
            attackFrames = 0;
        }
        if (attack)
        {
            if (attackFrames == 0)
            {
                controller.jumpAttack();
            }
        }
        //Attacking
        /*
        if (attackDown)
        {
            attackFrames = 7;
        }
        if (attackUp || !attack)
        {
            if (attackFrames > 0)
            {
                controller.attack();
            }
            attackFrames = 0;
        }
        if (attackFrames == 1 && attack)
        {
            attackFrames = 0;
            controller.jumpAttack();
        }
        */

        if (action)
        {
            controller.sheath();
        }
        if (block)
        {
            controller.blocking = true;
        }
        if (blockUp)
        {
            controller.blocking = false;
        }
        if (dodge)
        {
            controller.roll();
        }
	}
}
