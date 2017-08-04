using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {


    public float healthRemaining;
    public float maxHealth;
    public float currentSleep;
    public float maxSleep;
    public Image content;

    void Start()
    {
        maxHealth = GameObject.Find("Player").GetComponent<Stats>().maxHealth;
        maxSleep = GameObject.Find("Player").GetComponent<Stats>().maxSleep;
}
	// Update is called once per frame
	void Update () {
        ManageBar();
	}

    private void ManageBar()
    {
        if (gameObject.name == "HealthBar")
        {
            content.fillAmount = Map(healthRemaining, 0, maxHealth, 0, 1);
        }
        if (gameObject.name == "SleepBar")
        {
            content.fillAmount = Map(currentSleep, 0, maxSleep, 0, 1);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
