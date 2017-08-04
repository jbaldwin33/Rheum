using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnablePause : MonoBehaviour {

    public GameObject pauseMenuCanvas;
    public PauseMenu pauseMenu;
    // Update is called once per frame
    private void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();
    }
    void Update () {
		if(Input.GetButtonDown("Menu"))
        {
            pauseMenuCanvas.SetActive(true);
            pauseMenu.isPaused = true;
            transform.Translate(new Vector3(0, 0, 1));
        }
	}
    public void startGame()
    {
		print (true);
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
}
