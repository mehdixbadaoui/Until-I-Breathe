using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public List<GameObject> lights;
    public bool activated = false;

    public PlayableDirector Clip;


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
