using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceScript : MonoBehaviour {

    GameObject player;
    GameObject switch1;
    GameObject switch2;
    GameObject switch3;
    GameObject switch4;
    GameObject listener;

    public bool first = false;
    public bool second = false;
    public bool third = false;
    public bool fourth = false;
    
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        switch1 = GameObject.Find("SequenceSwitch1");
        switch2 = GameObject.Find("SequenceSwitch2");
        switch3 = GameObject.Find("SequenceSwitch3");
        switch4 = GameObject.Find("SequenceSwitch4");
        listener = GameObject.Find("EventListener");

        switch1.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
        switch2.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
        switch3.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
        switch4.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            listener.GetComponent<EventListenerScript>().onSwitch = true;
            if (gameObject == switch1)
            {
                listener.GetComponent<EventListenerScript>().seqSwitch1 = true;
                GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0);
            }
            if (gameObject == switch2 && listener.GetComponent<EventListenerScript>().seqSwitch1)
            {
                listener.GetComponent<EventListenerScript>().seqSwitch2 = true;
                GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0);
            }
            else if (gameObject == switch2 && !listener.GetComponent<EventListenerScript>().seqSwitch1)
            {
                listener.GetComponent<EventListenerScript>().seqSwitch1 = false;
                listener.GetComponent<EventListenerScript>().seqSwitch2 = false;
                switch1.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch2.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch3.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch4.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
            }
            if (gameObject == switch3 && listener.GetComponent<EventListenerScript>().seqSwitch1 && listener.GetComponent<EventListenerScript>().seqSwitch2)
            {
                listener.GetComponent<EventListenerScript>().seqSwitch3 = true;
                GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0);
            }
            else if (gameObject == switch3 && ((!listener.GetComponent<EventListenerScript>().seqSwitch1) || (!listener.GetComponent<EventListenerScript>().seqSwitch2)))
            {
                listener.GetComponent<EventListenerScript>().seqSwitch1 = false;
                listener.GetComponent<EventListenerScript>().seqSwitch2 = false;
                listener.GetComponent<EventListenerScript>().seqSwitch3 = false;
                switch1.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch2.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch3.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch4.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
            }
            if (gameObject == switch4 && listener.GetComponent<EventListenerScript>().seqSwitch1 && listener.GetComponent<EventListenerScript>().seqSwitch2 && listener.GetComponent<EventListenerScript>().seqSwitch3)
            {
                listener.GetComponent<EventListenerScript>().seqSwitch4 = true;
                GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0);
            }
            else if (gameObject == switch4 && ((!listener.GetComponent<EventListenerScript>().seqSwitch1) || (!listener.GetComponent<EventListenerScript>().seqSwitch2) || (!listener.GetComponent<EventListenerScript>().seqSwitch3)))
            {
                listener.GetComponent<EventListenerScript>().seqSwitch1 = false;
                listener.GetComponent<EventListenerScript>().seqSwitch2 = false;
                listener.GetComponent<EventListenerScript>().seqSwitch3 = false;
                listener.GetComponent<EventListenerScript>().seqSwitch4 = false;
                switch1.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch2.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch3.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
                switch4.GetComponent<MeshRenderer>().material.color = new Color(128f, 128f, 128f);
            }
        }
    }
}
