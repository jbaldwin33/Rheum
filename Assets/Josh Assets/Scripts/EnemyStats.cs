using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public bool killable;
    public float damageAmt = 10;
    public float currentHealth;
    private float timer;
    public float timeBetweenAttacks = 0.5f;
    private bool attack = false;
    Vector3 relativePosition;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks)
        {
            attack = true;
        } else
        {
            attack = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player" && attack)
        {
            other.gameObject.GetComponent<Stats>().takeDamage(damageAmt);
        }
    }
}
