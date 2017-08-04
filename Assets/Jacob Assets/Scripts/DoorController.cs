using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    //Public variables
    public bool open;
    public Vector3 offset;
    public float speed;

    //Private vars
    private Vector3 initPos;
    private bool prevOpen;
    public float currentPercent = 0f;

    //Components
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        initPos = transform.position;
        prevOpen = open;
	}
	
	// Update is called once per frame
	void Update () {


        if (prevOpen != open)
        {
            audioSource.Play();
        }
        prevOpen = open;

        if (open)
        {
            if (currentPercent < 1f)
            {
                currentPercent += speed * Time.deltaTime;
            }
            if (currentPercent > 1f)
            {
                currentPercent = 1f;
            }
        }
        else
        {
            if (currentPercent > 0f)
            {
                currentPercent -= speed * Time.deltaTime;
            }
            if (currentPercent < 0f)
            {
                currentPercent = 0f;
            }
        }
        transform.position = Vector3.Lerp(initPos, initPos + offset, currentPercent);
	}


}
