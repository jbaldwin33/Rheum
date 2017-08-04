using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    GameObject wall1;
    GameObject wall2;
    GameObject wall1back;
    GameObject wall2back;
    GameObject wall3;
    GameObject wall4;
    GameObject wall3back;
    GameObject wall4back;
    public bool openDoor1 = false;
    public bool openDoor2 = false;
    public bool openDoor3 = false;
    // Use this for initialization
    void Start () {

        wall1 = GameObject.Find("AOSWall1");
        wall2 = GameObject.Find("AOSWall2");
        wall1back = GameObject.Find("AOSWall1back");
        wall2back = GameObject.Find("AOSWall2back");
        wall3 = GameObject.Find("AOFWall3");
        wall4 = GameObject.Find("AOFWall4");
        wall3back = GameObject.Find("AOFWall3back");
        wall4back = GameObject.Find("AOFWall4back");
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.name == "AOFWalls")
        {
            if (openDoor1)
            {
                if (wall3.transform.localPosition.x > -27.3f)
                {
                    wall3.transform.Translate(new Vector3(-0.1f, 0, 0));
                    wall4back.transform.Translate(new Vector3(-0.1f, 0, 0));
                }
                if (wall4.transform.localPosition.x < -17.1f)
                {
                    wall4.transform.Translate(new Vector3(0.1f, 0, 0));
                    wall3back.transform.Translate(new Vector3(0.1f, 0, 0));
                }
            }
            else if (!openDoor1)
            {
                if (wall3.transform.localPosition.x < -27.3f)
                {
                    wall3.transform.Translate(new Vector3(0.1f, 0, 0));
                    wall4back.transform.Translate(new Vector3(0.1f, 0, 0));
                }
                if (wall4.transform.localPosition.x > -17.2f)
                {
                    wall4.transform.Translate(new Vector3(-0.1f, 0, 0));
                    wall3back.transform.Translate(new Vector3(-0.1f, 0, 0));
                }
            }
            if (openDoor2 && openDoor1)
            {
                if (wall3.transform.localPosition.x > -29.2f)
                {
                    wall3.transform.Translate(new Vector3(-0.1f, 0, 0));
                    wall4back.transform.Translate(new Vector3(-0.1f, 0, 0));
                }
                if (wall4.transform.localPosition.x < -15.2f)
                {
                    wall4.transform.Translate(new Vector3(0.1f, 0, 0));
                    wall3back.transform.Translate(new Vector3(0.1f, 0, 0));
                }
            }
        }
        else if (gameObject.name == "AOSWalls")
        {
            if (openDoor3)
            {
                if (wall1.transform.localPosition.x > -7f)
                {
                    wall1.transform.Translate(new Vector3(-0.1f, 0, 0));
                    wall2back.transform.Translate(new Vector3(-0.1f, 0, 0));
                }
                if (wall2.transform.localPosition.x < 7f)
                {
                    wall2.transform.Translate(new Vector3(0.1f, 0, 0));
                    wall1back.transform.Translate(new Vector3(0.1f, 0, 0));
                }
            }
            else if (!openDoor3)
            {
                if (wall1.transform.localPosition.x < -5.0f)
                {
                    wall1.transform.Translate(new Vector3(0.1f, 0, 0));
                    wall2back.transform.Translate(new Vector3(0.1f, 0, 0));
                }
                if (wall2.transform.localPosition.x > 5.0f)
                {
                    wall2.transform.Translate(new Vector3(-0.1f, 0, 0));
                    wall1back.transform.Translate(new Vector3(-0.1f, 0, 0));
                }
            }
        }
    }
    public void ShutDoor()
    {
        openDoor3 = false;
        if (wall1.transform.position.x < -5f)
        {
            wall1.transform.Translate(new Vector3(0.1f, 0, 0));
            wall2back.transform.Translate(new Vector3(0.1f, 0, 0));
        }
        if (wall2.transform.position.x > 5f)
        {
            wall2.transform.Translate(new Vector3(-0.1f, 0, 0));
            wall1back.transform.Translate(new Vector3(-0.1f, 0, 0));
        }
    }

    public void OpenDoor()
    {
        openDoor3 = true;
        if (wall1.transform.position.x > -7f)
        {
            wall1.transform.Translate(new Vector3(-0.1f, 0, 0));
            wall2back.transform.Translate(new Vector3(-0.1f, 0, 0));
        }
        if (wall2.transform.position.x < 7f)
        {
            wall2.transform.Translate(new Vector3(0.1f, 0, 0));
            wall1back.transform.Translate(new Vector3(0.1f, 0, 0));
        }
    }
}
