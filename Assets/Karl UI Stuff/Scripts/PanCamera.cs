using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour {

    public float panTime;
    public float panTimer;
    public bool right;
	// Use this for initialization
	void Start () {
        panTimer = panTime;
        right = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (panTimer >= 0)
        {
            if (right == true)
            {
                transform.Translate(new Vector3(.001f, 0, 0));
                panTimer -= Time.deltaTime;
            } else if (right == false)
            {
                transform.Translate(new Vector3(-.001f, 0, 0));
                panTimer -= Time.deltaTime;
            }
            
        } else
        {
            panTimer = panTime;
            right = !right;
        }
	}
}
