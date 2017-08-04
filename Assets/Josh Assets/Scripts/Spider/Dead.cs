using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class Dead : FSMState<SpiderScript>
{

    static readonly Dead instance = new Dead();

    public static Dead Instance
    {
        get
        {
            return instance;
        }
    }

    static Dead() { }
    private Dead() { }

    private NavMeshAgent agent;
    private AudioSource audio;
    private float timer;

    public override void Enter(SpiderScript s)
    {
        agent = s.GetComponent<NavMeshAgent>();
        audio = s.GetComponent<AudioSource>();
        timer = Time.timeSinceLevelLoad;
    }

    public override void Execute(SpiderScript s)
    {
        s.GetComponent<Animation>().Play("death1");
        if (Time.timeSinceLevelLoad - timer > 1f)
        {
            GameObject.Destroy(s.gameObject);
        }
        
    }

    public override void Exit(SpiderScript s)
    {
        //Debug.Log("Leaving firsst spot...");
    }
}