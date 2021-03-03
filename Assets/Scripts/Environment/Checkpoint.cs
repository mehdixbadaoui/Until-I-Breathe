using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private GameMaster gm;
    private bool alreadyChecked = false;
    private GameObject plane;


    // Start is called before the first frame update
    void Start()
    {
        // Game master
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        // Plane to visualize the checkpoint
        plane = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni" && !alreadyChecked )
        {
            gm.LastCheckPointPos = transform.position;
            alreadyChecked = true;

            // Change the color of the plane
            plane.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.blue);
        }
    }
}
