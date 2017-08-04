using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {

    private Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    private bool toEnd = true;
    Rigidbody body;

    private void Start()
    {
        startPos = transform.position;
        endPos = transform.position + endPos;
        body = transform.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (toEnd)
        {
            body.velocity = (endPos - transform.position).normalized * speed;
        } else
        {
            body.velocity = (startPos - transform.position).normalized * speed;
        }

        if (transform.position.x > endPos.x)
        {
            toEnd = false;
        }
        if (transform.position.x < startPos.x + 0.1f)
        {
            toEnd = true;
        }
    }
}
