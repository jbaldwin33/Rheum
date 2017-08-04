using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableWhenPause : MonoBehaviour {
    public GameObject player;
    public GameObject pauseMenu;
	// Update is called once per frame
	void Update () {
		if (pauseMenu.GetComponent<PauseMenu>().isPaused == true)
        {
            player.GetComponent<HumanoidController>().enabled = false;
        }
        if (pauseMenu.GetComponent<PauseMenu>().isPaused == false) { player.GetComponent<HumanoidController>().enabled = true; }
	}
}
