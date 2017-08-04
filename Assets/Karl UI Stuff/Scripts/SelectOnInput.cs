using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectOnInput : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;
    public GameObject selected;

    private bool usingKeyboard;
    private bool buttonSelected;
    private Vector3 mousePos;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.mousePosition != mousePos)
        {
            buttonSelected = false;
            EventSystem.current.SetSelectedGameObject(null);
            usingKeyboard = false;
            Cursor.visible = true;

        }

        mousePos = Input.mousePosition;

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            usingKeyboard = true;
        }

        if (usingKeyboard)
        {
            Cursor.visible = false;

        }

        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            usingKeyboard = true;
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

        selected = eventSystem.currentSelectedGameObject;
    }



    public void startGame()
    {
        //start scene here
        Application.LoadLevel("FinalScenes");
    }
    public void LoadCredits()
    {
        Application.LoadLevel("credits");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}

