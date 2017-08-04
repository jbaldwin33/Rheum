using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkPlatform : MonoBehaviour {

    //Public vars
    public int countDownTime = 7;
    public Vector3 movePos;
    public float speed = 2f;

    //Private vars
    private Vector3 initPos;
    private int baseTime = 200;
    private bool fallen = false;
    private int countDown = 0;

    //Components
    private AudioSource audioSource;



	// Use this for initialization
	void Start () {
        initPos = transform.position;
        movePos = transform.position + movePos;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (countDown > 0)
        {
            countDown--;
        }

        if (fallen && countDown <= (baseTime))
        {
            transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * speed);
        }
        if (countDown == 0)
        {
            if (fallen == true)
            {
                audioSource.Play();
            }
            fallen = false;
            transform.position = Vector3.Lerp(transform.position, initPos, Time.deltaTime * speed);
        }


	}


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (fallen == false)
            {
                audioSource.Play();
                fallen = true;
                countDown = baseTime +  countDownTime;
            }
        }
    }


}
