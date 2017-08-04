using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSwitchDoor : HitSwitch {

    public string action;
    public DoorController door;

	// Use this for initialization
	protected new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
	}

    protected override void trigger()
    {
        if (action == "toggle")
        {
            door.open = !door.open;
        }
        if (action == "close")
        {
            door.open = false;
        }
        if (action == "open")
        {
            door.open = true;
        }
    }
}
