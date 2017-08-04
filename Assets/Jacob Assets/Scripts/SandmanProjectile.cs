using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandmanProjectile : MonoBehaviour
{

    public float speed = 25f;
    public float damage = 20f;

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
            BossController boss = other.GetComponent<BossController>();
            if (boss)
            {
                
                boss.makeVulnerable(300);

            }
            stats.takeDamage(damage);
            GameObject.Destroy(transform.gameObject);
        }
        if (shieldScript && shieldScript.active)
        {
            HumanoidController cont = shieldScript.character.GetComponent<HumanoidController>();
            if (cont)
            {
                shieldScript.clang();
                transform.rotation = cont.lookRot;
                transform.position = transform.position + transform.forward * 1f;
            }
        }
        else
        {
            if (despawn < initDespawn - 50)
            {
                GameObject.Destroy(transform.gameObject);
            }
        }
    }
}
