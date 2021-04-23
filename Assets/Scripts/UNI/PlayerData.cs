using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData 
{
    public Scene scene;
    public string sceneName;
    public int sceneIndex;
    public float[] position; 

    public PlayerData()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        sceneIndex = scene.buildIndex;
        position = new float[3];
        position[0] = GameObject.FindGameObjectWithTag("uni").transform.position.x;
        position[1] = GameObject.FindGameObjectWithTag("uni").transform.position.y;
        position[2] = GameObject.FindGameObjectWithTag("uni").transform.position.z;

    }
}
