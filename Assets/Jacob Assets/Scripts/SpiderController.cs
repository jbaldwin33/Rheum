using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : Controller {

    //Controls
    public Vector3 inputDir;
    public float throttle;

    //Public vars
    public bool grounded;
    public bool shieldBreak;
    public ParticleSystem bloodSplatter;

    //Components
    private Animation anim;

    //Audio
    public AudioClip damage0;
    public AudioClip damage1;
    public AudioClip jump0;
    public AudioClip death0;

    //Private vars
    private float activeThrottle;
    private bool biteReady = false;
    private Vector3 groundNorm;
    private bool weakAttack;


    // Use this for initialization
    new void Start () {


        base.Start();
        anim = GetComponent<Animation>();
        collider.material.dynamicFriction = 3f;
        collider.material.staticFriction = 3f;
        anim.Play("idle");
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        updateLerps();
        updateState();


        if (movementLag <= 0)
        {
            if (inputDir.magnitude > 0.05f)
            {
                moveDir = Vector3.ProjectOnPlane(inputDir, groundNorm).normalized;

            }
            if (activeThrottle != throttle)
            {
                activeThrottle = throttle;
                updateAnim();
            }
        }
    }

    private void FixedUpdate()
    {
        updateGrounded();
        switch (state)
        {
            case "locomotion":
                rigidBody.MovePosition(transform.position + moveDir * Time.deltaTime * walkSpeed * activeThrottle);
                break;
            
        }
        if (state != "dead")
        {
            if (moveDir.magnitude < 0.05f)
            {
                moveDir = transform.forward;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 4f);
        }

    }

    void updateAnim()
    {
        switch(state)
        {
            case "locomotion":
                if (activeThrottle <= 0.1f)
                {
                    anim.Play("idle");
                    audioSource.Stop();
                }
                if (activeThrottle >= 0.8f)
                {
                    audioSource.Play();
                    anim.Play("run");
                }
                if (activeThrottle > 0.1f && activeThrottle < 0.8f)
                {
                    audioSource.Play();
                    anim.Play("walk");
                }
                break;
        }
    }

    void updateState()
    {
        switch(state)
        {
            case "locomotion":
                break;
            case "attacking":
                if (movementLag <= 0)
                {
                    biteReady = false;
                    state = "locomotion";
                    updateAnim();
                }
                break;
            case "jumping":
                if (grounded && movementLag <= 0)
                {
                    biteReady = false;
                    state = "locomotion";
                    updateAnim();
                }
                break;
            case "stunned":
                if (movementLag <= 0)
                {
                    state = "locomotion";
                    updateAnim();
                }
                break;
            case "dead":
                if (health > 0f)
                {
                    state = "locomotion";
                    updateAnim();
                }
                break;
        }
    }

    protected override void stun()
    {
        audioSource.Stop();
        movementLag = 50;
        actionLag = (int)(50f / attackSpeedMult);
        anim.Play("hit" + (UnityEngine.Random.Range(1, 3).ToString()));
        state = "stunned";
        biteReady = false;
    }

    protected override void hurt(float amount)
    {
        if (amount / stats.maxHealth >= stats.stunPercent)
        {
            stun();
        }
        if (amount > 3f)
        {
            int i = UnityEngine.Random.Range(0, 1);
            switch (i)
            {
                case 0:
                    audioSource.PlayOneShot(damage0);
                    break;
                case 1:
                    audioSource.PlayOneShot(damage1);
                    break;
                default:
                    break;
            }
            bloodSplatter.Play();
            renderer.material.color = Color.red;
            //biteReady = false;
        }
    }

    protected override void die()
    {
        audioSource.Stop();
        state = "dead";
        anim.Play("death1");
        audioSource.PlayOneShot(death0);
    }

    protected override void unDie()
    {
        state = "locomotion";
    }

    private void updateLerps()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime);
    }

    public void attack()
    {
        if (grounded && actionLag <= 0 && state == "locomotion")
        {
            audioSource.Stop();
            grounded = false;
            biteReady = true;
            movementLag = 45;
            actionLag = (int)(60f * 1f / stats.attackSpeedMult);
            rigidBody.velocity = inputDir.normalized * jumpHeight + Vector3.up * jumpHeight * 0.5f;
            moveDir = Vector3.ProjectOnPlane(inputDir, groundNorm).normalized;
            state = "attacking";
            anim.Play("attack1");
            audioSource.PlayOneShot(jump0);
        }
    }

    public void jump()
    {
        if (grounded && actionLag <= 0 && state == "locomotion")
        {
            audioSource.Stop();
            grounded = false;
            rigidBody.velocity = inputDir.normalized * jumpHeight + Vector3.up * jumpHeight;
            moveDir = Vector3.ProjectOnPlane(inputDir, groundNorm).normalized;
            movementLag = (int)(150f / attackSpeedMult);
            state = "jumping";
            anim.Play("jump");
            audioSource.PlayOneShot(jump0);
        }
    }

    void updateGrounded()
    {
        var layerMask = (1 << 8);
        layerMask |= (1 << 9);
        layerMask = ~layerMask;

        RaycastHit hit;
        bool newGrounded = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -Vector3.up, out hit, 0.3f * transform.lossyScale.y);
        groundNorm = hit.normal;
        if (Vector3.Dot(groundNorm, Vector3.up) < 0.8f)
        {
            grounded = false;
        }
        
        else
        {
            grounded = newGrounded;
        }

        if (grounded)
        {
            collider.material.staticFriction = 3f;
            collider.material.dynamicFriction = 3f;
        }
        else
        {
            collider.material.staticFriction = 0f;
            collider.material.dynamicFriction = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHit(other.transform);
    }
    private void OnCollisionEnter(Collision other)
    {
        OnHit(other.transform);
    }
    private void OnTriggerStay(Collider other)
    {
        OnHit(other.transform);
    }

    private void OnHit(Transform other)
    {
        if (biteReady)
        {
            Stats stats = other.GetComponent<Stats>();
            ShieldScript shieldScript = other.GetComponent<ShieldScript>();
            if (shieldScript)
            {
                Transform hitChar = shieldScript.character;
                if (shieldScript.active && shieldScript.isPlayers)
                {
                    shieldScript.clang();
                    biteReady = false;
                    Vector3 dir = Vector3.ProjectOnPlane((transform.position - other.position), Vector3.up).normalized;
                    rigidBody.velocity = dir * 4f;
                    stun();
                    if (shieldBreak)
                    {
                        shieldScript.character.GetComponent<Stats>().takeDamage(attackDamage/6f);
                    }
                }
            }
            if (stats && other.gameObject.tag == "Player")
            {
                biteReady = false;
                stats.takeDamage(attackDamage);
                actionLag = 50;
            }
        }
    }
}
