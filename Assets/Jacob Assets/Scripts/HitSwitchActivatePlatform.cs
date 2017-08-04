using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSwitchActivatePlatform : HitSwitch {

    public PlatformController controller;

	// Use this for initialization
	new void Start () {
        base.Start();
	}

    // Update is called once per frame
    new void Update () {
        base.Update();
	}

    protected override void trigger()
    {
        controller.active = true;
    }
}
