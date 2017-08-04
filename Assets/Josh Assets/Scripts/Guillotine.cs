using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : MonoBehaviour {

    public HingeJoint j;
    public JointMotor m;
    Transform axe;
    public float currentVelocity;
    GameObject cube;
    Vector3 relativePosition;

	// Use this for initialization
	void Start ()
    {
        axe = transform.GetChild(1);
        j = axe.GetComponent<HingeJoint>();
        m = new JointMotor();
        m.force = 5000;
        m.targetVelocity = 100f;
        j.motor = m;
        cube = GameObject.Find("Cube1");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (axe.rotation.eulerAngles.z > 216f && axe.rotation.eulerAngles.z < 219f)
        {
            m.targetVelocity = -100f;
            j.motor = m;
        }
        if (axe.rotation.eulerAngles.z < 148f && axe.rotation.eulerAngles.z > 145f)
        {
            m.targetVelocity = 100f;
            j.motor = m;
        }
	}
}
