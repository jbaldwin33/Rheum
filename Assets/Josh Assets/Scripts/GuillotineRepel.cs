using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuillotineRepel : MonoBehaviour {

    Vector3 relativePosition;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Cube1" || other.gameObject.name == "Player")
        {
            relativePosition = transform.InverseTransformPoint(other.transform.position);
            if (relativePosition.x > 0)
            {
                GetComponentInParent<Guillotine>().m.targetVelocity = 100f;
                GetComponentInParent<Guillotine>().j.motor = GetComponentInParent<Guillotine>().m;
            }
            else
            {
                GetComponentInParent<Guillotine>().m.targetVelocity = -100f;
                GetComponentInParent<Guillotine>().j.motor = GetComponentInParent<Guillotine>().m;
            }
        }
    }
}
