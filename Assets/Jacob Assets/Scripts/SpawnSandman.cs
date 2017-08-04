using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnSandman : MonoBehaviour {

    bool fightStarted = false;
    public GameObject sandman;
    public Vector3 initPos;
    private bool gameOver = false;
    public int countDown = 0;

    private GameObject currentSandman = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (countDown > 0)
        {
            countDown--;
        }
		if (fightStarted && (currentSandman == null || currentSandman.transform.GetComponent<Stats>().health <= 0))
        {
            if (gameOver == false)
            {
                countDown = 400;
            }
            if (countDown == 150)
            {
                Game_Helper.screenFader.alpha = 1f;
            }
            gameOver = true;

            
        }
        if (countDown == 0 && gameOver)
        {
            SceneManager.LoadScene("mainmenu");
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!fightStarted)
            {
                fightStarted = true;
                currentSandman = GameObject.Instantiate(sandman);
                currentSandman.transform.position = initPos;

            }
        }
    }
}
