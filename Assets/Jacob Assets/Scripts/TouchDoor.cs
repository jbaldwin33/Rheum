using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDoor : MonoBehaviour {

    public int openTime = 200;

    int timer = 0;

    private DoorController doorCont;

	// Use this for initialization
	void Start () {
        doorCont = GetComponent<DoorController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0)
        {
            timer--;
        }
        if (timer == 0)
        {
            doorCont.open = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (timer == 0 && doorCont.open == false)
            {
                timer = openTime;
                doorCont.open = true;
            }
        }
    }

}
