using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    public Vector3 startPos = new Vector3(463f, 102f, 146f);

    // Use this for initialization
    void Start () {
        SceneTransporter.sceneTransport(startPos, "Level00");
        Game_Helper.screenFader.forceBlack();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
