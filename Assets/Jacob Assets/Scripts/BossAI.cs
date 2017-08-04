using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    private BossController cont;
    private Transform target;
    private HumanoidController playerCont;
    private Stats stats;
    public float decisionTimeBase = 100;
    int decisionTimer = 0;

	// Use this for initialization
	void Start () {
        cont = GetComponent<BossController>();
        stats = GetComponent<Stats>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerCont = target.GetComponent<HumanoidController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (decisionTimer > 0)
        {
            decisionTimer--;
        }
        if (decisionTimer == 0)
        {

            int rng = UnityEngine.Random.Range(0, 100);
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist < 10f)
            {
                if (rng < 70)
                {
                    cont.defenseAttack();
                } else
                {
                    cont.startTeleport();
                }
                resetTimer();
                return;
            }

            if (cont.getMonsterCount() == 0)
            {
                cont.summonMonster();
                resetTimer();
                return;
            }

            if (rng < 40)
            {
                cont.fireballAttack();
            }
            if (rng > 40 && rng < 60)
            {
                cont.sandballAttack();
            }
            if (rng > 60 && rng < 90)
            {
                if (cont.getMonsterCount() < 3)
                {
                    
                    cont.summonMonster();
                }
            }
            if (rng > 90)
            {
                cont.startTeleport();
            }

            cont.forward = 1f;
            if (rng > 90)
            {
                cont.forward = -1f;
            }

            resetTimer();

        }
	}

    void resetTimer()
    {
        //Reset timer
        decisionTimer = (int)(decisionTimeBase * 0.5f + ((stats.health / stats.maxHealth) * 0.5f * decisionTimeBase));
        
    }

}
