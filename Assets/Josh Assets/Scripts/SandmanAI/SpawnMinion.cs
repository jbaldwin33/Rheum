using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpawnMinion : FSMState<SandmanAI> {

    static readonly SpawnMinion instance = new SpawnMinion();

    public static SpawnMinion Instance
    {
        get
        {
            return instance;
        }
    }

    static SpawnMinion() { }
    private SpawnMinion() { }

    float timer;
    GameObject spider;
    public override void Enter(SandmanAI s)
    {
        spider = GameObject.Find("Sandman").GetComponent<SandmanAI>().spider;
        timer = Time.timeSinceLevelLoad;
    }

    public override void Execute(SandmanAI s)
    {
        //Debug.Log("At first spot");
        s.gameObject.GetComponent<Animator>().SetBool("Standing", true);
        CreateMinion(s);
        //s.gameObject.GetComponent<Animator>().SetBool("Standing", false);
    }

    public override void Exit(SandmanAI s)
    {
        //Debug.Log("Leaving firsst spot...");
    }

    public void CreateMinion(SandmanAI s)
    {
        Debug.Log(Time.timeSinceLevelLoad);
        if (Time.timeSinceLevelLoad - timer > 3.5f)
        {
            GameObject clone1 = GameObject.Instantiate(spider, s.gameObject.transform.position + new Vector3(5, -7, 0), Quaternion.identity) as GameObject;
            GameObject clone2 = GameObject.Instantiate(spider, s.gameObject.transform.position + new Vector3(0, -7, 5), Quaternion.identity) as GameObject;
            GameObject clone3 = GameObject.Instantiate(spider, s.gameObject.transform.position + new Vector3(-5, -7, 5), Quaternion.identity) as GameObject;
            
            s.ChangeState(DoNothing.Instance);
        }
    }
}
