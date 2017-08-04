using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransporter : object {

    public static void sceneTransport(Vector3 pos, string scene)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject.DontDestroyOnLoad(player);
        GameObject.DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));


        GameObject[] objects = GameObject.FindGameObjectsWithTag("UI");
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject.DontDestroyOnLoad(objects[i]);
        }
        objects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].layer == 5)
            {
                GameObject.DontDestroyOnLoad(objects[i]);
            }
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        player.transform.position = pos;
        player.transform.GetComponent<RespawnManager>().respawnPos = pos;
        



    }
}
