using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTriggerScript : MonoBehaviour {

    public bool door = true;


	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (gameObject.name == "SandTrigger")
            {
                other.gameObject.GetComponent<Stats>().addSleep(other.gameObject.GetComponent<Stats>().maxSleep / 2);
                door = !door;
            }
        }
    }
}
