using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class vignetteControl : MonoBehaviour {
    [SerializeField]
    VignetteAndChromaticAberration vignetteController;

    [SerializeField]
    GameObject player;
    Stats stats;

    float sleep;
	// Use this for initialization
	void Start () {
        stats = player.GetComponent<Stats>();
        sleep = stats.sleep;
	}
	
	// Update is called once per frame
	void Update () {
        sleep = stats.sleep;
        vignetteController.intensity = Map(sleep, 0f, stats.maxSleep, 0, 0.7f);
	}

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
