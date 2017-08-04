using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal : MonoBehaviour
{

    public Transform torchesToDisable;
    public Transform torchesToEnable;
    public Vector3 targetPos;
    public Vector3 moveDir = Vector3.forward;
    public int framesDelay = 150;

    private GameObject player;

    private int countDown = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            countDown--;
        }
        if (countDown == framesDelay / 2)
        {
            Game_Helper.screenFader.alpha = 0f;
            disableTorches();
            enableTorches();
            if (player)
            {
                player.transform.position = targetPos;
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = targetPos;
                HumanoidController cont = player.transform.GetComponent<HumanoidController>();
                if (cont)
                {
                    cont.throttle = 0;
                    cont.setMoveDir(moveDir);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && countDown == 0)
        {
            HumanoidController controller = other.transform.GetComponent<HumanoidController>();
            if (controller)
            {
                Game_Helper.screenFader.alpha = 1f;
                controller.setMoveDir(Vector3.ProjectOnPlane((transform.position - other.transform.position), Vector3.up).normalized);
                controller.throttle = 0.5f;
                controller.addMovementLag(framesDelay / 2);
                countDown = framesDelay;
            }
        }
    }

    private void disableTorches()
    {
        if (torchesToDisable)
        {
            for (int i = 0; i < torchesToDisable.childCount; i++)
            {
                Transform trans = torchesToDisable.GetChild(i);
                trans.Find("Particle System").GetComponent<Light>().enabled = false;
            }
        }
    }

    private void enableTorches()
    {
        if (torchesToEnable)
        {
            for (int i = 0; i < torchesToEnable.childCount; i++)
            {
                Transform trans = torchesToEnable.GetChild(i);
                trans.Find("Particle System").GetComponent<Light>().enabled = true;
            }
        }
    }
}
