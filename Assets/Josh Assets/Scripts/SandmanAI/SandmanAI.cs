using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandmanAI : MonoBehaviour {

    private FiniteStateMachine<SandmanAI> FSM;
    private RaycastHit hit;


    public AINavSteeringController aiSteer;
    public GameObject spider;


    public void Awake()
    {
        aiSteer = this.gameObject.GetComponent<AINavSteeringController>();
        //Debug.Log("Guard awakes...");
        FSM = new FiniteStateMachine<SandmanAI>();
        FSM.Configure(this, DoNothing.Instance);
    }

    public void ChangeState(FSMState<SandmanAI> e)
    {
        FSM.ChangeState(e);
    }

    public void Update()
    {
        FSM.Update();
        //Debug.Log(FSM.CurrentState);
    }
}
