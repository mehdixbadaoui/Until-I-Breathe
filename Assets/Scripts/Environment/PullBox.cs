using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    private bool can_pull;
    public GameObject box;
    public float force = 4f;

    private void Start()
    {
        can_pull = true;
    }

    public void Pull()
    {
        if (can_pull)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            box.GetComponent<Rigidbody>().AddForce(force, 0, 0);
            can_pull = false;
        }
    }
}
