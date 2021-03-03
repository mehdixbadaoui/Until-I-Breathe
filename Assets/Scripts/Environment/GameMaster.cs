using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //Position of the last checkpoint
    private Vector3 lastCheckPointPos;
    private static GameMaster instance;

    private GameObject uni;

    // Set and get of the last checkoint position
    public Vector3 LastCheckPointPos
    {
        get { return lastCheckPointPos; }   // get method
        set { lastCheckPointPos = value; }  // set method
    }


    void Awake()
    {
        //To check if the gameMaster doesn't exist for the moment
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        //Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");
    }

    // If Uni die
    public void Die()
    {
        uni.transform.position = lastCheckPointPos;
    }

}
