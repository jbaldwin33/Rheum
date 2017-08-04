using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpiderAttack : FSMState<SpiderScript> {

    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10;


    Animation anim;
    GameObject player;
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    float timer;

    static readonly SpiderAttack instance = new SpiderAttack();

    public static SpiderAttack Instance
    {
        get
        {
            return instance;
        }
    }

    static SpiderAttack() { }
    private SpiderAttack() { }


    public override void Enter(SpiderScript s)
    {

    }

    public override void Execute(SpiderScript s)
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && s.gameObject.GetComponent<InRange>().playerInRange)
        {
            Attack(s);
        }
        if (!s.gameObject.GetComponent<InRange>().playerInRange)
        {
            s.ChangeState(Chase.Instance);
        }
    }

    public override void Exit(SpiderScript s)
    {
        //Debug.Log("Leaving firsst spot...");
    }


    public void Attack(SpiderScript s)
    {
        timer = 0f;
        s.GetComponent<Animation>().Play("attack1");
        if (GameObject.Find("Player").GetComponent<Stats>().health > 0)
        {
            GameObject.Find("Player").GetComponent<Stats>().takeDamage(attackDamage);
        }
    }
}
