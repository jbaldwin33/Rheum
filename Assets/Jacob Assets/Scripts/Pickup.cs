using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public float health;
    public float maxHealth = 0f;
    public float magicka;
    public float sleep;
    public bool despawn = true;


    //Components
    private AudioSource audioSource;
    private Collider collider;
    private Rigidbody rigidBody;
    public Renderer rend;
    private Transform itemModel;


    private bool taken = false;
    private int despawnTimer;



	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
        itemModel = transform.GetChild(0);
        despawnTimer = 900;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (despawnTimer > 0)
        {
            despawnTimer--;
        }
        if (despawnTimer == 0 && despawn)
        {
            GameObject.Destroy(transform.gameObject);
        }
        itemModel.Rotate(Vector3.up, 1f);
	}


    private void FixedUpdate()
    {
        if (!taken)
        {
            rigidBody.AddForce(Vector3.up * 5f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!taken && (other.gameObject.tag == "Player"))
        {
            audioSource.Play();
            despawnTimer = 50;
            rigidBody.isKinematic = true;
            collider.isTrigger = true;
            taken = true;
            rend.enabled = false;
            Stats stats = other.transform.GetComponent<Stats>();
            stats.addHealth(health);
            stats.addMagicka(magicka);
            stats.addSleep(-sleep);
            stats.maxHealth += maxHealth;
        }
    }
}
