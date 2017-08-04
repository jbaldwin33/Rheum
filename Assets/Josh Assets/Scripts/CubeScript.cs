using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour
{
    public bool onQuicksand;
    GameObject player;
    Vector3 direction;
    RaycastHit hit;
    Ray myRay;
    static Vector3 relativePosition;
    Vector3 cubeOrigin;
    bool isGrounded;
    public bool isPushing = false;
    bool inside = false;

    void Start()
    {
        player = GameObject.Find("Player");
        hit = new RaycastHit();
        cubeOrigin = transform.position;

    }

    void FixedUpdate()
    {
        if (GameObject.Find("IllusionCube").transform.localPosition.y > -5.5f)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
        if (!isGrounded)
        {
            GameObject.Find("IllusionCube").GetComponent<Rigidbody>().drag = 0;
        }
        else
        {
            GameObject.Find("IllusionCube").GetComponent<Rigidbody>().drag = 200;
        }
        string floortag;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            floortag = hit.collider.gameObject.tag;
            if (floortag == "quicksand")
            {
                onQuicksand = true;
            }
            else
            {
                onQuicksand = false;
            }
        }
        if (onQuicksand)
        {
            Sink();
        }
    }

    public void Sink()
    {
        if (!inside)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().drag = 10;
        }
        if (transform.localPosition.y < 20.0f)
        {
            transform.position = cubeOrigin;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<Rigidbody>().drag = 200;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player)
        {
            inside = true;
            relativePosition = transform.InverseTransformPoint(collision.transform.position);
            player.GetComponent<PlayerEnvironment>().isPushing = true;
            if (gameObject.name == "IllusionCube" || gameObject.name == "block (1)" || gameObject.name == "block (2)" || gameObject.name == "block (3)" || gameObject.name == "block (4)" || gameObject.name == "block (5)" || gameObject.name == "block")
            {
                if (relativePosition.x > 0)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                }
                else
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                }
                if (relativePosition.z > 0 && relativePosition.x > -0.6f && relativePosition.x < 0.6f)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                }
                else if (relativePosition.z < 0 && relativePosition.x > -0.6f && relativePosition.x < 0.6f)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                }
            }
            else
            {
                if (relativePosition.x > 0 && relativePosition.z > -0.6f && relativePosition.z < 0.6f)
                {
                    //Debug.Log("left");
                    player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
                }
                else if (relativePosition.x < 0 && relativePosition.z > -0.6f && relativePosition.z < 0.6f)
                {
                    //Debug.Log("right");
                    player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
                }
                else if (relativePosition.z > 0 && relativePosition.x > -0.6f && relativePosition.x < 0.6f)
                {
                    //Debug.Log("front");
                    player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                }
                else if (relativePosition.z < 0 && relativePosition.x > -0.6f && relativePosition.x < 0.6f)
                {
                    //Debug.Log("behind");
                    player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                }
                /*else
                {
                    player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                }*/
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inside = false;
            player.GetComponent<PlayerEnvironment>().isPushing = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
