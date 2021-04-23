using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;

    public bool OnOff = false;

    // Start is called before the first frame update
    void Start()
    {
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnOff)
            RagOn();
        else
            RagOff();
    }

    public void RagOn()
    {
        //wait 2-3 seconds.
        foreach (Collider col in rigColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = false;
        }
    }


    public void RagOff()
    {
        //wait 2-3 seconds.
        foreach (Collider col in rigColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

}
