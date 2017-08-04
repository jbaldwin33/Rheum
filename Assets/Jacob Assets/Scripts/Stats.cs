using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float health = 100f;
    public float maxHealth = 100f;
    public float magicka = 100f;
    public float maxMagicka = 100f;
    public float sleep = 0;
    public float maxSleep = 2000f;

    //Combat stats
    public float attack = 30f;
    public float defense = 1f;
    public float walkSpeed = 6f;
    public float jumpHeight = 6f;
    public float attackSpeedMult = 1f;
    public float stunPercent = 0.15f;

    public int invincFrames = 0;

    private void Update()
    {
        if (invincFrames > 0)
        {
            invincFrames--;
        }
    }


    //Use these to interact with the stats
    public void takeDamage(float damage)
    {
        if (invincFrames > 0)
        {
            return;
        }
        health -= damage * 1f/defense;
        constrainStats();
    }

    public void consumeMagicka(float loss)
    {
        magicka -= loss;
        constrainStats();
    }

    public void addHealth(float toAdd)
    {
        health += toAdd;
        constrainStats();
    }

    public void addMagicka(float toAdd)
    {
        magicka += toAdd;
        constrainStats();
    }

    public void addSleep(float toAdd)
    {
        sleep += toAdd;
        constrainStats();
    }

    public void depleteSleep(float toAdd)
    {
        sleep -= toAdd;
        constrainStats();
    }

    //Keeps stats from going over max value, or under 0f
    private void constrainStats()
    {
        if (health < 0f)
        {
            health = 0f;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (magicka < 0f)
        {
            magicka = 0f;
        }
        if (magicka > maxMagicka)
        {
            magicka = maxMagicka;
        }
        if (sleep < 0f)
        {
            sleep = 0f;
        }
        if (sleep > maxSleep)
        {
            sleep = maxSleep;
        }
    }



}
