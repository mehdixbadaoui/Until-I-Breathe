using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public GameObject light;


    // Update is called once per frame
    void Update()
    {

    }

    public void Unlock()
    {
        if(door)
        {
            door.GetComponent<Door>().locked = false;
            light.GetComponent<Light>().color = Color.green;

        }

    }
}
