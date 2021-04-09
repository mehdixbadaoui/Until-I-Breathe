using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("box"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
