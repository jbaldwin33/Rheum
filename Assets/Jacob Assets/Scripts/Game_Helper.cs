using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Helper : MonoBehaviour {

    public static GameObject playerUI;
    public static ScreenFader screenFader;
    public static GameObject player;
    public static GameObject mainCamera;

	// Use this for initialization
	void Start () {
        playerUI = transform.gameObject;
        screenFader = playerUI.transform.Find("ScreenFade").GetComponent<ScreenFader>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
