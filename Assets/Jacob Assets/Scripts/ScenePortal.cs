using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour {

    public Vector3 targetPos;
    public string targetScene;
    public int framesDelay = 150;

    private int countDown = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (countDown > 0)
        {
            countDown--;
        }
        if (countDown == framesDelay/2)
        {
            Game_Helper.screenFader.alpha = 0f;
            SceneTransporter.sceneTransport(targetPos, targetScene);
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
                controller.addMovementLag(framesDelay);
                countDown = framesDelay;
            }
        }
    }
}
