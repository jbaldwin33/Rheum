using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    //Public vars
    public bool crit;
    public bool active;
    public bool actionState = false;
    public float damage;
    public bool isPlayers;
    public Transform character;

    //Private vars
    private HumanoidController controller;
    private int timer = 0;
    private MeleeWeaponTrail trailScript;
    

    private void Start()
    {
        controller = character.GetComponent<HumanoidController>();
        trailScript = GetComponent<MeleeWeaponTrail>();
    }

    private void FixedUpdate()
    {
        trailScript.Emit = actionState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            Stats stats = other.GetComponent<Stats>();
            ShieldScript shieldScript = other.GetComponent<ShieldScript>();
            if (shieldScript)
            {
                if (shieldScript.isPlayers != isPlayers)
                {
                    Transform hitChar = shieldScript.character;
                    if (!hitChar || (hitChar != character && shieldScript.active))
                    {
                        shieldScript.clang();
                        if (!crit)
                        {
                            active = false;
                        }
                    }
                }
            }
            if (stats)
            {
                if (!isPlayers && !(other.gameObject.tag == "Player"))
                {
                    return;
                }
                if (crit)
                {
                    stats.takeDamage(damage * 1.7f);
                } else
                {
                    stats.takeDamage(damage);
                }
                crit = false;
                //active = false;
            }
        }
    }
}
