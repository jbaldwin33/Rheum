using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Controller {

    //States
    //0 locomotion
    //1 vulnerable
    //2 castAim
    //3 castArea
    //4 dead

    public float forward = 0f;

    private Vector3 initPos;
    private GameObject player;
    private Transform target;
    private HumanoidController targetCont;

    public float arenaSize = 20f;

    //Components
    private Animator anim;
    public ParticleSystem sand;
    public ParticleSystem flash;
    public ParticleSystem splatter;
    public GameObject fireBall;
    public GameObject sandBall;
    public Transform sandPoofTrans;
    private ParticleSystem sandPoof;
    private AudioSource sandPoofAud;

    //Audio
    public AudioClip laserBlast;
    public AudioClip fireballAud;
    public AudioClip whoosh;

    public AudioClip damage0;
    public AudioClip damage1;
    public AudioClip death;

    //Monsters
    public GameObject soldier;
    public GameObject spider;
    public ArrayList monsters;

    public int vulnerableFrames = 0;
    private float forwardLerp = 0f;


	// Use this for initialization
	new void Start () {
        base.Start();
        monsters = new ArrayList();
        movementLag = 70;
        sandPoof = sandPoofTrans.GetComponent<ParticleSystem>();
        sandPoofAud = sandPoofTrans.GetComponent<AudioSource>();
        initPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        targetCont = target.GetComponent<HumanoidController>();
        state = "locomotion";
        anim = GetComponent<Animator>();



    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        updateState();
        updateCounters();
        if ((state == "locomotion" || state == "castAim") && movementLag <= 0)
        {
            Vector3 targetRot = Vector3.ProjectOnPlane((target.position - transform.position), Vector3.up).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRot), Time.deltaTime * 10f);
        }
        forwardLerp = Mathf.Lerp(forwardLerp, forward, Time.deltaTime * 5f);
        anim.SetFloat("forward", forwardLerp);

    }

    private void updateCounters()
    {
        if (vulnerableFrames > 0)
        {
            vulnerableFrames--;
        }
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime * 3f);
    }

    private void updateState()
    {
        switch(state)
        {
            case "locomotion":
                anim.SetInteger("state", 0);
                if (vulnerableFrames > 0)
                {
                    state = "vulnerable";
                }

                break;
            case "vulnerable":
                forward = 0f;
                anim.SetInteger("state", 1);
                if (vulnerableFrames <= 0)
                {
                    state = "locomotion";
                }
                break;
            case "castArea":
                forward = 0f;
                anim.SetInteger("state", 3);
                if (movementLag <= 0)
                {
                    state = "locomotion";
                }
                break;
            case "castAim":
                forward = 0f;
                anim.SetInteger("state", 2);
                if (actionLag <= 0)
                {
                    state = "locomotion";
                }
                break;
            case "dead":
                forward = 0f;
                anim.SetInteger("state", 4);
                break;
        }

        if (state != "vulnerable" && state != "dead")
        {
            if (!sand.isPlaying)
            {
                sand.Play();
            }
            stats.invincFrames = int.MaxValue;
        } else
        {
            stats.invincFrames = 0;
            if (sand.isPlaying)
            {
                sand.Stop();
            }
        }
    }

    protected override void stun()
    {
        anim.SetTrigger("stun");
        movementLag += 50;
    }

    public void makeVulnerable(int frames)
    {
        sand.Stop();
        hurt(10f);
        stun();
        state = "vulnerable";
        vulnerableFrames = frames;
    }

    protected override void die()
    {
        anim.SetTrigger("die");
        audioSource.PlayOneShot(death);
        state = "dead";
    }

    protected override void hurt(float amount)
    {
        stun();
        int i = UnityEngine.Random.Range(0, 2);
        if (i == 0)
        {
            audioSource.PlayOneShot(damage0);
        }
        if (i == 1)
        {
            audioSource.PlayOneShot(damage1);
        }
        splatter.Play();
        renderer.material.color = Color.red;
    }

    protected override void unDie()
    {
        state = "locomotion";
    }

    public void fireballAttack()
    {
        if (movementLag <= 0 && actionLag <= 0 && state == "locomotion")
        {
            anim.SetTrigger("fireballAttack");
            state = "castAim";
            actionLag = 65;
        }
    }

    public void sandballAttack()
    {
        if (movementLag <= 0 && actionLag <= 0 && state == "locomotion")
        {
            anim.SetTrigger("sandballAttack");
            state = "castAim";
            actionLag = 105;
        }
    }

    public void defenseAttack()
    {
        if (movementLag <= 0 && actionLag <= 0 && state == "locomotion")
        {
            anim.SetTrigger("defenseAttack");
            state = "castArea";
            movementLag = 99;
        }
    }

    public void summonMonster()
    {
        if (movementLag <= 0 && actionLag <= 0 && state == "locomotion")
        {
            anim.SetTrigger("summonMonster");
            state = "castArea";
            movementLag = 115;
        }
    }

    public void startTeleport()
    {
        if (movementLag <= 0 && actionLag <= 0 && state == "locomotion")
        {
            anim.SetTrigger("teleport");
            state = "castArea";
            movementLag = 85;
        }
    }

    private void flashBoom()
    {
        flash.Play();
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < 10f)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            rb.velocity += (target.position - transform.position).normalized * 10f + Vector3.up * 4f;
            Stats stat = target.GetComponent<Stats>();
            stat.takeDamage(40);
        }
    }

    private void shootFireball()
    {
        GameObject fire = GameObject.Instantiate(fireBall);
        Vector3 dir = (target.position - transform.position).normalized;
        fire.transform.position = transform.position + dir * 2f + Vector3.up * 1.3f;
        fire.transform.rotation = Quaternion.LookRotation(dir);
        audioSource.PlayOneShot(fireballAud);
    }

    private void shootSandball()
    {
        GameObject fire = GameObject.Instantiate(sandBall);
        Vector3 dir = (target.position - transform.position).normalized;
        fire.transform.position = transform.position + dir * 2f + Vector3.up * 1.3f;
        fire.transform.rotation = Quaternion.LookRotation(dir);
        audioSource.PlayOneShot(whoosh);
    }

    private void spawnMonster()
    {
        Vector3 pos = initPos + new Vector3(UnityEngine.Random.Range(-arenaSize, arenaSize), 0f, UnityEngine.Random.Range(-arenaSize, arenaSize));
        int rng = UnityEngine.Random.Range(0, 10);
        if (rng < 3)
        {
            GameObject monst = GameObject.Instantiate(soldier);
            monst.transform.position = pos;
            monsters.Add(monst);
        } else
        {
            GameObject monst = GameObject.Instantiate(spider);
            monst.transform.position = pos;
            monsters.Add(monst);
        }
        sandPoofTrans.position = pos + Vector3.up * 0.5f;
        sandPoofAud.Play();
        sandPoof.Play();
    }

    private void Teleport()
    {
        Vector3 pos = initPos + new Vector3(UnityEngine.Random.Range(-arenaSize, arenaSize), 0f, UnityEngine.Random.Range(-arenaSize, arenaSize));
        transform.position = pos;
        sandPoofTrans.position = pos + Vector3.up * 0.5f;
        sandPoofAud.Play();
        sandPoof.Play();
    }

    private void playFlashAudio()
    {
        audioSource.PlayOneShot(laserBlast);
    }

    public int getMonsterCount()
    {
        int num = 0;
        for (int i = 0; i < monsters.Count; i++)
        {
            if ((GameObject)monsters[i] != null)
            {
                num++;
            }
        }
        return num;
    }

}
