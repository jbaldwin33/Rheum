using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class Chase : FSMState<SpiderScript> {

    static readonly Chase instance = new Chase();

    public static Chase Instance
    {
        get
        {
            return instance;
        }
    }

    static Chase() { }
    private Chase() { }

    public override void Enter(SpiderScript s)
    {

    }

    public override void Execute(SpiderScript s)
    {
        s.GetComponent<Animation>().Play();
        s.GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Player").transform.position);
        if (Time.timeSinceLevelLoad > 2)
        {
            //Debug.Log("At first spot");
            
        }
    }

    public override void Exit(SpiderScript s)
    {
        //Debug.Log("Leaving firsst spot...");
    }
}
