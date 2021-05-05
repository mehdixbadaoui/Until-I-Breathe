using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;
    public List<Vector3> previousPos;

    public bool OnOff = false;

    // Start is called before the first frame update
    void Start()
    {
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigRigidbodies)
        {
            previousPos.Add(rb.transform.position);
        }

        if (OnOff)
            RagOn();
        else
            RagOff();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (OnOff)
            RagOn();
        else
            RagOff();
        */
    }

    public void AddForceToRagdoll(Vector3 vector)
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.AddForce(vector);
        }
    }

    public void RagOn()
    {
        OnOff = true;

        //wait 2-3 seconds.
        foreach (Collider col in rigColliders)
        {
            col.enabled = true;
        }

        int countRb = 0;
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = false;
            previousPos[countRb] = rb.transform.position;
            countRb += 1;
        }
    }


    public void RagOff()
    {
        OnOff = false;

        //wait 2-3 seconds.
        foreach (Collider col in rigColliders)
        {
            col.enabled = false;
        }

        int countRb = 0;
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.transform.position = previousPos[countRb];
            countRb += 1;
        }
    }

}
