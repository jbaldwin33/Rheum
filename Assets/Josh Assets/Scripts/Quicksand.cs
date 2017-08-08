using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour {

    GameObject player;
    Vector3 originalPosition;
    public float y;
    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
        originalPosition = transform.position;
        y = GetComponent<BoxCollider>().center.y;
    }

    // Update is called once per frame
    void Update() {
        if (player.GetComponent<PlayerEnvironment>().onQuicksand)
        {
            if (GetComponent<BoxCollider>().center.y > -0.3)
            {
                y = BoxTranslate(y);
                GetComponent<BoxCollider>().center = new Vector3(0, y, 0);
            }
        }
        else
        {
            //y = originalPosition.y;
            GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        }
    }

    float BoxTranslate(float y)
    {
        return y -= 0.02f;

    }
}
