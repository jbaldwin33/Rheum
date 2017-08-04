using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    public CameraController camCont;

    public float mouseSensitivity = 7;
    public int keys;


    public void updateMouseSensitivity(float value)
    {
        mouseSensitivity = Map(value, 0, 1, 1, 10);
    }


    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    private void Update()
    {
        camCont.sensitivityX = mouseSensitivity;
        camCont.sensitivityY = mouseSensitivity * 5f / 7f;
    }
}