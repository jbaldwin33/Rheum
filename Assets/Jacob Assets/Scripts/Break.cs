using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Break : MonoBehaviour {

    //Public vars
    public Renderer rend;
    public ParticleSystem particles;
    public int respawnTime = 500;
    public int respawnTimer = 0;

    //Pickups
    public GameObject heart;

    //Components
    private AudioSource audioSource;
    private Stats stats;
    new private Collider collider;

    //Private vars
    private bool broken = false;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        stats = GetComponent<Stats>();
        collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (respawnTimer == 1 && broken)
        {
            respawn();
        }

        if (respawnTimer > 0)
        {
            respawnTimer--;
        }


		if (stats.health <= 0)
        {
            if (!broken)
            {
                destroy();
            }
        }
	}

    private void respawn()
    {
        broken = false;
        rend.enabled = true;
        stats.health = stats.maxHealth;
        collider.isTrigger = false;
    }

    private void destroy() {
        collider.isTrigger = true;
        audioSource.Play();
        particles.Play();
        rend.enabled = false;
        broken = true;
        respawnTimer = respawnTime;


        int rng = UnityEngine.Random.Range(0, 100);
        GameObject pickup;
        if (rng < 50)
        {
            pickup = Instantiate(heart);
            pickup.transform.position = transform.position + new Vector3(0f, 1f, 0f);
            Rigidbody rb = pickup.transform.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(0f, 3f), UnityEngine.Random.Range(-2f, 2f));
        }
    }
}
