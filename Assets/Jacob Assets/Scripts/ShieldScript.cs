using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {
    public Transform character;
    public bool active;
    public bool isPlayers;
    private AudioSource audioSource;

    public AudioClip clang0;
    public AudioClip clang1;
    public ParticleSystem flash;



    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clang()
    {
        flash.Play();
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                audioSource.PlayOneShot(clang0);
                break;
            case 1:
                audioSource.PlayOneShot(clang1);
                break;
        }
    }
}


