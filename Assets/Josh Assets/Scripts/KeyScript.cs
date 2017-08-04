using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeyScript : MonoBehaviour {

    public AudioClip clip;


	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.GetComponent<HumanoidController>().keys++;
            AudioSource.PlayClipAtPoint(clip, transform.position);
            GameObject.Destroy(gameObject);
        }
    }
}
