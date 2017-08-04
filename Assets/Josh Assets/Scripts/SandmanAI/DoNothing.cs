using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DoNothing : FSMState<SandmanAI> {

    static readonly DoNothing instance = new DoNothing();

    public static DoNothing Instance
    {
        get
        {
            return instance;
        }
    }

    static DoNothing() { }
    private DoNothing() { }

    public override void Enter(SandmanAI s)
    {
        s.gameObject.GetComponent<Animator>().SetBool("Standing", false);
    }

    public override void Execute(SandmanAI s)
    {
        /*if (Input.GetKeyDown("v"))
        {
            //Debug.Log("At first spot");
            s.ChangeState(SpawnMinion.Instance);
        }*/
    }

    public override void Exit(SandmanAI s)
    {
        //Debug.Log("Leaving firsst spot...");
    }
}
