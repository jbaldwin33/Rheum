using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour {

    //Components
    NavMeshAgent agent;
    SpiderController controller;
    Stats stats;

    //Player
    Transform target;
    Stats enemyStats;
    HumanoidController playerController;

    public string aiState;
    public float maxDistance;
    public bool pause;

    //Private vars
    float distPlayer;

    // Use this for initialization
    void Start () {
        aiState = "idle";
        controller = GetComponent<SpiderController>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<Stats>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStats = target.GetComponent<Stats>();
        playerController = target.GetComponent<HumanoidController>();
        agent.speed = stats.walkSpeed * 1.1f;
    }
	
	// Update is called once per frame
	void Update () {

        pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseMenu>().isPaused;

        if (pause)
        {
            Time.timeScale = 0.0f;
            return;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        updateState();
        slowAgent();

        distPlayer = Vector3.Distance(transform.position, target.position);

    }

    void updateState()
    {
        int rng = UnityEngine.Random.Range(0, 100);
        switch(aiState)
        {
            case "idle":
                controller.throttle = 0f;
                if (distPlayer <= maxDistance && playerController.state != "dead")
                {
                    aiState = "attackPlayer";
                }
                break;
            case "attackPlayer":
                
                agent.SetDestination(target.position);
                if (distPlayer > maxDistance)
                {
                    controller.throttle = 0f;
                    aiState = "idle";
                }
                
                if (distPlayer < stats.jumpHeight * 0.75f)
                {
                    controller.throttle = 0.5f;
                    controller.inputDir = (target.position - transform.position).normalized;
                    controller.attack();
                } else
                {
                    controller.throttle = 1f;
                    controller.inputDir = (target.position - agent.nextPosition).normalized;
                }
                
                if (playerController.state == "dead")
                {
                    aiState = "idle";
                }
                break;
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
