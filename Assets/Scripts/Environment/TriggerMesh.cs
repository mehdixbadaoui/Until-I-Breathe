using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMesh : MonoBehaviour
{
    public GameObject Object;

    MeshRenderer MeshOfObject;

    // Start is called before the first frame update
    void Start()
    {
        MeshOfObject = Object.GetComponent<MeshRenderer>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("uni"))
        {
            MeshOfObject.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            MeshOfObject.enabled = true;
        }
    }
}
