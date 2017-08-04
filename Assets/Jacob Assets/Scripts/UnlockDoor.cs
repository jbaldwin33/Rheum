using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{

    bool opened = false;

    private DoorController doorCont;

    // Use this for initialization
    void Start()
    {
        doorCont = GetComponent<DoorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !opened)
        {
            HumanoidController cont = other.transform.GetComponent<HumanoidController>();
            if (cont.keys > 0)
            {
                cont.keys--;
                opened = true;
                doorCont.open = true;
            }
        }
    }
}
