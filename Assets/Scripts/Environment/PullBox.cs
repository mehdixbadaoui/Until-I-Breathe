using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    public GameObject box;
    public float force = 4f;

    private void Start()
    {

    }

    public void Pull()
    {
        Debug.Log("puuuuulllll");
        GetComponent<Rigidbody>().isKinematic = false;
        box.GetComponent<Rigidbody>().AddForce(force, 0, 0);

    }
}
