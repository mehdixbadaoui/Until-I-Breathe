using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    public List<GameObject> Lights;
    public GameObject Camera;
    public Transform door1, door2;
    public float doorDist;
    public Transform NextLevelPos;
    public GameObject NExtLevelPrefab;

    public void BO()
    {
        //TURN OFF LIGHTS
        foreach (GameObject light in Lights)
            light.GetComponent<Light>().intensity = 0;

        //DISABLE CAMERA
        Camera.GetComponent<PatrolDrones>().enabled = false;
        Camera.GetComponent<BoxDamage>().LightsOut();

        //MOVE DOORS
        door1.transform.position += Vector3.forward * doorDist;
        door2.transform.position += Vector3.back * doorDist;

        //CREATE NEXT LEVEL TRIGGERBOX
        Instantiate(NExtLevelPrefab, NextLevelPos);
    }
}
