using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandballScript : MonoBehaviour
{

    public float speed = 29f;
    public float damage = 30f;

    private Rigidbody rigidBody;
    private int initDespawn = 900;
    private int despawn = 900;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (despawn > 0)
        {
            despawn--;
        }
        if (despawn == 0)
        {
            GameObject.Destroy(transform.gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        onHit(other.transform);
    }

    private void OnCollisionEnter(Collision other)
    {
        onHit(other.transform);

    }

    private void onHit(Transform other)
    {
        Stats stats = other.GetComponent<Stats>();
        ShieldScript shieldScript = other.GetComponent<ShieldScript>();
        if (stats)
        {
            stats.takeDamage(damage);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity += Vector3.up * 3f + (rigidBody.velocity).normalized * 8f;
            }
            destruct();
        }
        if (shieldScript && shieldScript.active)
        {
            Stats stat = shieldScript.character.GetComponent<Stats>();
            Rigidbody rb = shieldScript.character.GetComponent<Rigidbody>();
            if (stat)
            {
                shieldScript.clang();
                stat.takeDamage(damage * 0.3f);
                if (rb)
                {
                    rb.velocity += Vector3.up * 2f + (rigidBody.velocity).normalized * 4f;
                }
                destruct();
            }
        }
        else
        {
            if (despawn < initDespawn - 50)
            {
                destruct();
            }
        }
    }

    private void destruct()
    {
        transform.position = transform.position = new Vector3(0f, -500f, 0f);
        despawn = 150;
    }
}
