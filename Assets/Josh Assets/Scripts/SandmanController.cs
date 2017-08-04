using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SandmanController : Controller
{

    //public string state;                       //Current state the player is in
    //0 freeMove
    //1 combatReady
    //2 blocking
    //3 attacking
    //4 dead
    //5 stunned
    //6 roll


    //Controls
    public Vector3 inputDir;
    public Quaternion lookRot;
    public float throttle;
    public bool blocking;

    //Public vars
    public ParticleSystem bloodSplatter;
    public bool grounded;

    //Components
    private Animator anim;

    //Audio clips
    public AudioClip damage0;
    public AudioClip damage1;
    public AudioClip damage2;
    public AudioClip swing0;
    public AudioClip swing1;
    public AudioClip swing2;
    public AudioClip grunt0;
    public AudioClip grunt1;
    public AudioClip death;
    public AudioClip attach;




    //Private variables
    private float speedMult = 1f;               //Speed multiplier
    private Vector3 groundNorm;                 //Normal of the ground which the player is standing on
    private Quaternion moveRot;
    private float speed;                        //How fast the character is moving
    bool attacking = false;
    private Quaternion animLookRot;
    private bool equipped;                  //Whether sword/shield are equipped
    private Quaternion headLook;
    private float activeThrottle;          //Throttle to be used
    private float leftRight = 0f;           //Used in falling animation, nothing else


    //Skeleton parts
    private Transform head;
    private Transform hips;
    private Transform spine;
    private Transform chest;
    private Transform upperChest;
    private Transform rightHand;
    private Transform leftHand;
    private Transform rightLowerArm;
    private Transform leftLowerArm;

    //Lerps
    private Quaternion lookRotLerp;
    private float animSplit = 0f;       //Whether using the base layer, or the two split layers
    private float animSplitLerp = 0f;
    private float speedLerp;
    private Quaternion animLookRotLerp;
    private Quaternion headLookLerp;
    private float leftRightLerp = 0f;

    // Use this for initialization
    new void Start()
    {

        base.Start();
        //Components
        anim = GetComponent<Animator>();
        stats = GetComponent<Stats>();

        //Skeleton
        head = anim.GetBoneTransform(HumanBodyBones.Head);
        hips = anim.GetBoneTransform(HumanBodyBones.Hips);
        spine = anim.GetBoneTransform(HumanBodyBones.Spine);
        chest = anim.GetBoneTransform(HumanBodyBones.Chest);
        upperChest = anim.GetBoneTransform(HumanBodyBones.UpperChest);
        rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand);
        leftHand = anim.GetBoneTransform(HumanBodyBones.LeftHand);
        leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        rightLowerArm = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);

        //Variables
        state = "freeMove";
        lookRotLerp = new Quaternion();
        

    }

    protected new void Update()
    {
        base.Update();
        updateLerps();
        updateState();

        if (movementLag <= 0)
        {
            if (inputDir.magnitude > 0.05f)
            {
                moveDir = inputDir;

            }
            activeThrottle = throttle;
        }
        moveRot = Quaternion.LookRotation(moveDir);


        //Anim
        speed = Vector3.Dot(transform.forward, moveDir) * walkSpeed * activeThrottle * speedMult;
        leftRight = Vector3.Dot(transform.right, moveDir);
        anim.SetFloat("speed", speedLerp / transform.lossyScale.y);
        anim.SetLayerWeight(1, animSplitLerp);
        anim.SetFloat("leftRight", leftRightLerp);
        //anim.SetLayerWeight(2, animSplitLerp);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        updateGrounded();

        //Rotation
        switch (state)
        {
            case "stun":
            case "attacking":
            case "roll":
                speedMult = 0f;
                rigidBody.MovePosition(rigidBody.position + Vector3.ProjectOnPlane(anim.deltaPosition, groundNorm));
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 10f);
                break;
            case "freeMove":
                speedMult = 1f;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 10f);
                rigidBody.MovePosition(transform.position + Vector3.ProjectOnPlane(moveDir, groundNorm).normalized * Time.deltaTime * walkSpeed * activeThrottle * speedMult);
                break;
            case "blocking":
                speedMult = 0.7f;
                goto case "combatReady";
            case "combatReady":
                if (state != "blocking")
                {
                    speedMult = 0.9f;
                }
                if (inputDir.magnitude < 0.05f)
                {
                    moveDir = Vector3.ProjectOnPlane(lookRot * Vector3.forward, Vector3.up);
                }
                rigidBody.MovePosition(transform.position + Vector3.ProjectOnPlane(moveDir, groundNorm).normalized * Time.deltaTime * walkSpeed * activeThrottle * speedMult);
                float f = moveRot.eulerAngles.y - lookRot.eulerAngles.y;
                if (f < 0f)
                {
                    f += 360f;
                }
                if (f < 91f || f > 271f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 10f);
                }
                else
                {
                    Vector3 v = moveDir * -1f;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(v), Time.deltaTime * 10f);
                }
                break;

        }

    }

    public void LateUpdate()
    {
        updateSkeleton();
    }

    void updateState()
    {

        switch (state)
        {
            case "freeMove":
                animSplit = 0f;
                anim.SetInteger("state", 0);
                break;
            case "combatReady":
                animSplit = 1f;
                anim.SetInteger("state", 1);
                if (blocking)
                {
                    state = "blocking";
                }
                break;
            case "blocking":
                animSplit = 1f;
                anim.SetInteger("state", 2);
                if (!blocking)
                {
                    state = "combatReady";
                }
                break;
            case "attacking":
                anim.speed = attackSpeedMult;
                animSplit = 0f;
                anim.SetInteger("state", 3);
                if (movementLag <= 0)
                {
                    state = "combatReady";
                }
                break;
            case "dead":
                anim.SetInteger("state", 4);
                animSplit = 0f;
                if (stats.health > 0f)
                {
                    state = "stunned";
                }
                break;
            case "stun":
                animSplit = 0f;
                anim.SetInteger("state", 5);
                if (movementLag <= 0)
                {
                    if (equipped)
                    {
                        state = "combatReady";
                    }
                    else
                    {
                        state = "freeMove";
                    }
                }
                break;
            case "roll":
                animSplit = 0f;
                anim.speed = attackSpeedMult;
                anim.SetInteger("state", 6);
                if (movementLag <= 0)
                {
                    if (equipped)
                    {
                        state = "combatReady";
                    }
                    else
                    {
                        state = "freeMove";
                    }
                }
                break;

        }
        if (!(state == "attacking") && !(state == "hitBlocked"))
        {
            anim.speed = 1f;
        }
        if (stats.health <= 0f)
        {
            if (state != "dead")
            {
                audioSource.PlayOneShot(death);
                state = "dead";
            }
        }

    }


    void updateLag()
    {
        if (actionLag > 0)
        {
            actionLag--;
        }
        if (movementLag > 0)
        {
            movementLag--;
        }
        if (movementLag > actionLag)
        {
            actionLag = movementLag;
        }
    }

    void updateLerps()
    {
        lookRotLerp = Quaternion.Slerp(lookRotLerp, lookRot, Time.deltaTime * 10f);
        animSplitLerp = Mathf.Lerp(animSplitLerp, animSplit, Time.deltaTime * 8f * attackSpeedMult);
        speedLerp = Mathf.Lerp(speedLerp, speed, Time.deltaTime * 10f);
        animLookRotLerp = Quaternion.Slerp(animLookRotLerp, animLookRot, Time.deltaTime * 10f * stats.attackSpeedMult);
        headLookLerp = Quaternion.Slerp(headLookLerp, headLook, Time.deltaTime * 10f);
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime);
        leftRightLerp = Mathf.Lerp(leftRightLerp, leftRight, Time.deltaTime * 7f);
    }

    private void updateSkeleton()
    {


        switch (state)
        {
            case "freeMove":
                animLookRot = spine.rotation;
                break;
            case "combatReady":
                Vector3 offsetEuler = new Vector3(0f, 30f, 0f);
                Vector3 delt = lookRot.eulerAngles - transform.eulerAngles;
                animLookRot = Quaternion.Euler(spine.eulerAngles + new Vector3(0f, delt.y, 0f) + offsetEuler);
                break;
            case "attacking":
                animLookRot = Quaternion.Euler(spine.eulerAngles + new Vector3(lookRot.eulerAngles.x, 0f, 0f));
                break;
            case "dead":
                animLookRot = spine.rotation;
                break;
            default:
                animLookRot = spine.rotation;
                break;
        }

        spine.rotation = animLookRotLerp;

        if (state != "dead" && state != "roll")
        {
            if ((Vector3.Dot(lookRot * Vector3.forward, upperChest.forward) > 0f))
            {
                headLook = lookRot;
            }
            else
            {
                headLook = transform.rotation;
            }
            head.rotation = headLookLerp;
        }



    }

    void updateGrounded()
    {
        var layerMask = (1 << 8);
        layerMask |= (1 << 9);
        layerMask = ~layerMask;

        RaycastHit hit;
        bool newGrounded = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -Vector3.up, out hit, 0.3f);
        groundNorm = hit.normal;
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
        if (Vector3.Dot(groundNorm, Vector3.up) < 0.7f)
        {
            grounded = false;
        }
        else
        {
            grounded = newGrounded;
        }
        anim.SetBool("grounded", grounded);
    }

    public void jump()
    {
        if (grounded && state != "dead")
        {
            anim.SetTrigger("jump");
            rigidBody.velocity += new Vector3(0f, jumpHeight, 0f);
            playGrunt();
            //rigidBody.velocity += moveDir * stats.walkSpeed * speedMult * activeThrottle;
        }

        grounded = false;
    }

    public void attack()
    {

        if (actionLag <= 0 && state != "attacking" && state != "dead")
        {
            anim.SetTrigger("attack");
            state = "attacking";
            movementLag = (int)(30f / attackSpeedMult);
            actionLag = (int)(1.3f * movementLag);
            moveDir = Vector3.ProjectOnPlane(lookRot * Vector3.forward, Vector3.up);
        }
    }



    protected override void hurt(float amount)
    {
        if (amount > 3f)
        {
            int i = UnityEngine.Random.Range(0, 3);
            switch (i)
            {
                case 0:
                    audioSource.PlayOneShot(damage0);
                    break;
                case 1:
                    audioSource.PlayOneShot(damage1);
                    break;
                case 2:
                    audioSource.PlayOneShot(damage2);
                    break;
                default:
                    break;
            }
            actionLag = (int)(stats.attackSpeedMult * 20f);
            animLookRotLerp = Quaternion.Euler(spine.rotation.eulerAngles + new Vector3(UnityEngine.Random.Range(-25f, 25f), UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f)));
            bloodSplatter.Play();
        }
        if (amount / stats.maxHealth >= stats.stunPercent)
        {
            stun();
        }
    }

    protected override void die()
    {
        if (state == "dead")
        {
            return;
        }
        audioSource.PlayOneShot(death);
        anim.SetTrigger("die");
    }

    protected override void unDie()
    {
        state = "stun";
        anim.SetTrigger("unDie");
        addMovementLag(100);
    }

    private void playGrunt()
    {
        int i = UnityEngine.Random.Range(0, 2);
        switch (i)
        {
            case 0:
                audioSource.PlayOneShot(grunt0);
                break;
            case 1:
                audioSource.PlayOneShot(grunt1);
                break;
        }
    }


    protected override void stun()
    {
        anim.SetTrigger("stun");
        movementLag = (int)(30f / attackSpeedMult);
        playGrunt();
        state = "stun";
    }

    public void LeftDown()
    {
        GameObject.Find("EventListener").GetComponent<EventListenerScript>().leftDown = true;
    }
    public void RightDown()
    {
        GameObject.Find("EventListener").GetComponent<EventListenerScript>().rightDown = true;
    }
}
