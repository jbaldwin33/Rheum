using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEmitterScript : MonoBehaviour {

    public bool emitEvent = false;
    public string myEventMsg = "Hello";

    // Update is called once per frame
    void Update()
    {
        
            emitEvent = false;
            EventManager.TriggerEvent<SimpleEvent, string>(myEventMsg);
    }
}
