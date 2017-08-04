using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{


    public bool isPaused;

    public EventSystem eventSystem;
    public GameObject pauseMenuCanvas;
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public GameObject initialSelected_Settings;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            pauseMenuCanvas.SetActive(false);
        }

        if (Input.GetButtonDown("Menu"))
        {
            isPaused = !isPaused;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    public void Resume()
    {
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Application.LoadLevel("mainmenu");
    }

    public void LoadCredits()
    {
		Application.LoadLevel("credits");
    }

    public void showOptionsMenu()
    {

        mainMenu.SetActive(false);
        optionsMenu.gameObject.SetActive(true);

        eventSystem.SetSelectedGameObject(initialSelected_Settings);
        eventSystem.UpdateModules();
    }

    public void returnToPauseMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }
}
