using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSoldierAI : MonoBehaviour {

    //Components
    NavMeshAgent agent;
    HumanoidController controller;
    Stats stats;
    Animator anim;
    //Player
    Transform target;
    Stats enemyStats;
    HumanoidController playerController;
    Animator playerAnim;

    //Public vars
    public string aiState;
    public float maxDistance;

    //Private vars
    float distPlayer;
    int timer = 0;

    // Use this for initialization
    void Start () {
        aiState = "idle";
        controller = GetComponent<HumanoidController>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStats = target.GetComponent<Stats>();
        playerController = target.GetComponent<HumanoidController>();
        playerAnim = GetComponent<Animator>();
        agent.speed = stats.walkSpeed * 1.1f;
    }
	
	// Update is called once per frame
	void Update () {

        updateState();
        slowAgent();
        
        if (controller.state == "freeMove")
        {
            controller.attack();
        }

        distPlayer = Vector3.Distance(transform.position, target.position);
    }

    void updateState()
    {
        int rng = UnityEngine.Random.Range(0, 100);
        int rng1 = UnityEngine.Random.Range(0, 100);
        Vector3 dirTarg = (target.position - transform.position).normalized;
        switch (aiState)
        {
            case "idle":
                controller.throttle = 0f;
                if (distPlayer <= maxDistance && playerController.state != "dead")
                {
                    aiState = "attackPlayer";
                }
                break;
            case "attackPlayer":
                controller.lookRot = Quaternion.LookRotation(dirTarg);
                agent.SetDestination(target.position);
                if (distPlayer < 7f * transform.lossyScale.y)
                {
                    controller.blocking = true;
                }
                else
                {
                    controller.blocking = false;
                }
                if (distPlayer < 1.7f * transform.lossyScale.y)
                {
                    controller.throttle = 0f;
                    controller.inputDir = (target.position - transform.position).normalized;
                    if (rng < 7)
                    {
                        if (rng1 < 20)
                        {
                            controller.jumpAttack();
                        }
                        else
                        {
                            controller.attack();
                        }
                    }
                }
                if (rng == 7 && rng1 < 40)
                {
                    controller.roll();
                }
                else
                {
                    controller.throttle = 1f;
                    Vector3 moveDir = (agent.nextPosition - transform.position).normalized;
                    controller.inputDir = moveDir;
                }
                if (distPlayer > maxDistance)
                {
                    aiState = "idle";
                }
                if (playerController.state == "dead")
                {
                    aiState = "idle";
                }
                break;

                /*
                case "idle":
                    controller.throttle = 0f;
                    if (distPlayer <= maxDistance)
                    {
                        aiState = "attackPlayer";
                    }
                    break;
                case "attackPlayer":
                    controller.lookRot = Quaternion.LookRotation(dirTarg);
                    agent.SetDestination(target.position);
                    if (distPlayer < 5f)
                    {
                        controller.blocking = true;
                    } else
                    {
                        controller.blocking = false;
                        if (rng < 2)
                        {
                            controller.roll();
                        }
                    }
                    if (distPlayer < 1.5f)
                    {
                        controller.throttle = 0f;
                        controller.inputDir = (target.position - transform.position).normalized;
                        if (rng < 20)
                        {
                            controller.jumpAttack();
                        } else
                        {
                            controller.attack();
                        }
                    }
                    else
                    {
                        controller.throttle = 1f;
                        Vector3 moveDir = (agent.nextPosition - transform.position).normalized;
                        controller.inputDir = moveDir;
                    }
                    if (rng < 2)
                    {
                        aiState = "attackDefensive";
                    }
                    if (distPlayer > maxDistance)
                    {
                        aiState = "idle";
                    }
                    break;
                case "attackDefensive":
                    controller.lookRot = Quaternion.LookRotation(dirTarg);
                    controller.blocking = true;

                    if (distPlayer > 10f)
                    {
                        agent.SetDestination(target.position);
                    } else
                    {
                        Vector3 dirRight = (Quaternion.AngleAxis(-45f, Vector3.up) * dirTarg).normalized;
                        agent.SetDestination(transform.position + dirRight * 3f);
                    }
                    if (distPlayer < 1.5f)
                    {
                        if (rng < 3)
                        {
                            if (rng < 20)
                            {
                                controller.jumpAttack();
                            } else
                            {
                                controller.attack();
                            }
                        }
                    }


                    if (rng < 2)
                    {
                        aiState = "attackPlayer";
                    }
                    if (distPlayer > maxDistance)
                    {
                        aiState = "idle";
                    }

                    break;
                    */
        }
    }

    //Keeps agent from getting away
    void slowAgent()
    {
        Vector3 delt = agent.nextPosition - transform.position;
        if (delt.magnitude > agent.radius)
        {
            agent.nextPosition = transform.position + 0.9f * delt;
        }
    }
}
