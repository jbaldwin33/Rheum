using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsInput : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;
    public Slider mouseSensitivity;
    public GameObject settings;

    private float oldSensitivity;
    private bool buttonSelected;
    // Use this for initialization
    void Start()
    {
        GameSettings settings_values = settings.GetComponent<GameSettings>();
        oldSensitivity = settings_values.mouseSensitivity;
        mouseSensitivity.value = Map(oldSensitivity, 1, 10, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

    }
    public void Save()
    {
        settings.GetComponent<GameSettings>().updateMouseSensitivity(mouseSensitivity.value);
    }

    public void Add()
    {
        mouseSensitivity.value += .1f;
    }

    public void Subtract()
    {
        mouseSensitivity.value -= .1f;
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
