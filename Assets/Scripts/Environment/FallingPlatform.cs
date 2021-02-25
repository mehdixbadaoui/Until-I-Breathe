using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "uni")
            gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
        //gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
