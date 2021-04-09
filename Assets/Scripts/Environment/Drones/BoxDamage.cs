using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamage : MonoBehaviour
{
    public List<Light> Lights;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("box"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            foreach(Light light in Lights)
            {
                light.intensity = 0;
            }
        }
    }
}
