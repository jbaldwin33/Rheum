using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class displayKeys : MonoBehaviour {

    [SerializeField]
    HumanoidController player;
    Text displayText;
	// Use this for initialization
	void Start () {
        displayText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        displayText.text = string.Format("KEYS: {0}", player.keys);
	}
}
