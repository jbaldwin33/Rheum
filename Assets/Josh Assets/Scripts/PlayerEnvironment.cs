using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnvironment : MonoBehaviour {

    public bool onQuicksand = false;

    //Game Objects
    private GameObject[] illusions;
    public GameObject health;
    public GameObject sleep;
    public bool isPushing = false;
    void Start () {

        //Game Objects
        illusions = GameObject.FindGameObjectsWithTag("illusion");
        health = GameObject.Find("HealthBar");
        sleep = GameObject.Find("SleepBar");
    }
	
	// Update is called once per frame
	void Update () {
        AddSleep();
        CalcHealth();
        IsOnQuicksand();
        SeeIllusion();
    }

    public void AddSleep()
    {
        if (onQuicksand)
        {
            GetComponent<Stats>().addSleep(3f);
        }
        sleep.GetComponent<BarScript>().currentSleep = GetComponent<Stats>().sleep;
    }

    public void CalcHealth()
    {
        health.GetComponent<BarScript>().healthRemaining = GetComponent<Stats>().health;
    }

    public void IsOnQuicksand()
    {
        RaycastHit hit;
        string floortag;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            floortag = hit.collider.gameObject.tag;
            if (floortag == "quicksand")
            {
                onQuicksand = true;
                GetComponent<Stats>().walkSpeed = 4;
            }
            else
            {
                onQuicksand = false;
                GetComponent<Stats>().walkSpeed = 6;
            }
        }
    }

    public void SeeIllusion()
    {
        if ((GetComponent<Stats>().sleep / GetComponent<Stats>().maxSleep) > 0.5)
        {
            foreach (GameObject i in illusions)
            {
                if (i.gameObject.name == "Stairs")
                {
                    i.GetComponent<BoxCollider>().enabled = false;
                    i.GetComponent<MeshCollider>().enabled = true;
                    i.GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    i.GetComponent<BoxCollider>().enabled = true;
                    i.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        else if ((GetComponent<Stats>().sleep / GetComponent<Stats>().maxSleep) < 0.5)
        {
            foreach (GameObject i in illusions)
            {
                if (i.gameObject.name == "Stairs")
                {
                    i.GetComponent<BoxCollider>().enabled = false;
                    i.GetComponent<MeshCollider>().enabled = false;
                    i.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    i.GetComponent<BoxCollider>().enabled = false;
                    i.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }
}
