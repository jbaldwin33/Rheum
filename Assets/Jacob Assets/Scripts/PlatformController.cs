using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public bool active = false;
    public int timeAtDest = 50;
    public float speed;
    public Vector3 endPos;
    private Vector3 startPos;


    public AudioClip stop;


    private bool toStart = false;
    int countDown = 50;

    //Components
    private AudioSource audioSource;
    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
        endPos = startPos + endPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (countDown > 0 && active)
        {
            countDown--;
        }

		if (countDown == 0 && active)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (toStart)
            {
                if (Vector3.Distance(transform.position, startPos) > 0.5f)
                {
                    rigidBody.velocity = (startPos - transform.position).normalized * speed;
                } else
                {
                    rigidBody.velocity = Vector3.zero;
                    toStart = false;
                    countDown = timeAtDest;
                    audioSource.Stop();
                    audioSource.PlayOneShot(stop);
                }
            } else
            {
                if (Vector3.Distance(transform.position, endPos) > 0.5f)
                {
                    rigidBody.velocity = (endPos - transform.position).normalized * speed;
                }
                else
                {
                    rigidBody.velocity = Vector3.zero;
                    toStart = true;
                    countDown = timeAtDest;
                    audioSource.Stop();
                    audioSource.PlayOneShot(stop);
                }
            }
        }
	}
}
