using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class SpiderScript : MonoBehaviour {

    private Animation anim;
    private BoxCollider col;
    private NavMeshAgent nav;
    private Transform player;
    private FiniteStateMachine<SpiderScript> FSM;

    public void Awake()
    {
        anim = GetComponent<Animation>();
        col = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        FSM = new FiniteStateMachine<SpiderScript>();
        FSM.Configure(this, Chase.Instance);
    }

    public void ChangeState(FSMState<SpiderScript> s)
    {
        FSM.ChangeState(s);
    }

    public void Update()
    {
        WasHit();
        if (GetComponent<InRange>().playerInRange)
        {
            FSM.ChangeState(SpiderAttack.Instance);
        }
        FSM.Update();
        transform.LookAt(player);
        //Vector3 direction = GameObject.Find("Player").transform.position - transform.position;
        //Debug.DrawRay(transform.position, direction, Color.red);
    }

    public void WasHit()
    {
        if (GetComponent<InRange>().playerInRange && player.GetComponent<HumanoidController>().state == "attacking")
        {
            gameObject.GetComponent<Stats>().takeDamage(player.GetComponent<Stats>().attack);
        }
    }
}
