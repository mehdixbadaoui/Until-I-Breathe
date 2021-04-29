using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Lever : MonoBehaviour
{
    public GameObject door;
    public List<GameObject> lights;
    public bool activated = false;
    public Transform Grill;

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

    public IEnumerator RotateDoor()
    {
        float elapsedTime = 0;
        Transform t = Grill;
        var originalRotation = Grill.rotation;
        //t.rotation = Quaternion.Euler(-90, 0, 0);
        while(elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            Grill.rotation = Quaternion.Lerp(originalRotation, Quaternion.Euler(-90, 0, 0), elapsedTime / 1);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("rotate");
    }
}
