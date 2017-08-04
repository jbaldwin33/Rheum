using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {

    //Public vars
    public string state;
    public bool canDelete;

    //Components
    protected Stats stats;
    public new Renderer renderer;
    protected AudioSource audioSource;
    protected Rigidbody rigidBody;
    protected new Collider collider;

    //Protected vars
    protected int actionLag;
    protected int movementLag;
    protected Vector3 moveDir = Vector3.forward;

    //Stats
    protected float health;
    protected float magicka;
    protected float walkSpeed;
    protected float jumpHeight;
    protected float attackSpeedMult;
    protected float attackDamage;

    //Private vars
    private float prevHealth = 0f;
    private int deathTimer = 0;

    // Use this for initialization
    protected virtual void Start () {
        audioSource = GetComponent<AudioSource>();
        actionLag = 0;
        movementLag = 0;
        stats = GetComponent<Stats>();
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        prevHealth = stats.health;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        updateLag();
        updateStats();
        if (health < prevHealth)
        {
            hurt(prevHealth - health);
        }
        if (health <= 0 && prevHealth > 0)
        {
            die();
            deathTimer = 700;
        }
        if (health > 0 && prevHealth <= 0)
        {
            unDie();
            deathTimer = 0;
        }
        prevHealth = health;
        if (state == "dead" && canDelete)
        {
            if (deathTimer == 200)
            {
                rigidBody.isKinematic = true;
                collider.isTrigger = true;
            }
            if (deathTimer < 200)
            {
                transform.position += -0.01f * Vector3.up;
            }
            if (deathTimer == 0)
            {
                GameObject.Destroy(transform.gameObject);
            }
        }
        if (state == "dead" && health > 0)
        {
            rigidBody.isKinematic = false;
            collider.isTrigger = false;
            movementLag = 100;
        }


	}

    private void updateLag()
    {
        if (movementLag > actionLag)
        {
            actionLag = movementLag;
        } 
        if (actionLag > 0)
        {
            actionLag--;
        }
        if (movementLag > 0)
        {
            movementLag--;
        }
        if (deathTimer > 0)
        {
            deathTimer--;
        }
    }

    public void addMovementLag(int amount)
    {
        movementLag += amount;
    }

    public void addActionLag(int amount)
    {
        actionLag += amount;
    }
    
    public void setMoveDir(Vector3 dir)
    {
        moveDir = dir.normalized;
    }


    private void updateStats()
    {
        health = stats.health;
        magicka = stats.magicka;
        walkSpeed = stats.walkSpeed;
        jumpHeight = stats.jumpHeight;
        attackSpeedMult = stats.attackSpeedMult;
        attackDamage = stats.attack;
        
    }

    protected abstract void hurt(float amount);

    protected abstract void die();

    protected abstract void unDie();

    protected abstract void stun();

    private void despawn()
    {
        deathTimer = 200;
    }

}
