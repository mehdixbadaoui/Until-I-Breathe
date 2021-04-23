﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private GameMaster gm;
    public bool alreadyChecked = false;
    private GameObject plane;

    // platforms
    public Platforms[] platformsToReset;

    // platforms
    public GameObject[] fogToReset;
    public float heightToResetFog = -5f;


    // Start is called before the first frame update
    void Start()
    {
        // Game master
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();


        // Plane to visualize the checkpoint
        if (transform.childCount != 0)
            plane = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni" && !alreadyChecked )
        {
            //Debug.Log(gm.LastCheckPointPos);
            gm.LastCheckPointPos = new Vector3 ( other.transform.position.x , transform.position.y , transform.position.z );
            gm.lastCheckpoint = this;
            //Debug.Log(gm.LastCheckPointPos);
            alreadyChecked = true;

            // Change the color of the plane
            if (plane != null)
                plane.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.blue);
        }
    }

    private void OnDrawGizmos()
    {
        if (fogToReset.Length > 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position + new Vector3(0 , heightToResetFog , 0), 1f);
        }

    }
}
