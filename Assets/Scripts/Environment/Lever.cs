using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public List<GameObject> lights;


    // Update is called once per frame

    public void Unlock()
    {
        if(door)
        {            
            door.GetComponent<Door>().locked = false;
            foreach(GameObject light in lights)
                light.GetComponent<Light>().color = Color.green;

        }

    }
}
