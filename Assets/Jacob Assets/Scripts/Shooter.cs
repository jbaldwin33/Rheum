using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public bool active = true;

    public int shootCycleFrames;
    public GameObject projectile;

    private AudioSource audioSource;

    int timer = 0;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (timer > 0)
        {
            timer--;
        }
		if (timer == 0)
        {
            if (active) 
            Shoot();
        }
	}

    private void Shoot()
    {
        audioSource.Play();
        timer = shootCycleFrames;
        GameObject proj = GameObject.Instantiate(projectile);
        proj.transform.rotation = transform.rotation;
        proj.transform.position = transform.position + transform.forward * 3f;
    }
}
