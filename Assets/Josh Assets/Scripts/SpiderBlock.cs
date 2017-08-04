using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderBlock : MonoBehaviour {

    GameObject player;
    Vector3 direction;
    RaycastHit hit;
    Vector3 relativePosition;
    bool isGrounded;

    void Start()
    {
        player = GameObject.Find("Player");
        hit = new RaycastHit();
        

    }

    void FixedUpdate()
    {
        
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            
            relativePosition = transform.InverseTransformPoint(collision.transform.position);
//            Debug.Log(relativePosition);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            /*if (Input.GetKey("m"))
            {
                if (relativePosition.x > 0)
                {
                    Debug.Log("front");
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    
                }
                else
                {
                    Debug.Log("back");
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                }
                if (relativePosition.z > 0 && relativePosition.x > -0.4f && relativePosition.x < 0.4f)
                {
                    Debug.Log("left");
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                }
                else if (relativePosition.z < 0 && relativePosition.x > -0.4f && relativePosition.x < 0.4f)
                {
                    Debug.Log("right");
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                }
            }*/
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}
