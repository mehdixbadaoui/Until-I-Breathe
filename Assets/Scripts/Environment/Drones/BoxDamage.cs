using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamage : MonoBehaviour
{
    public List<Light> Lights;
    public List<GameObject> SpotsGeants;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("box"))
        {
            Fall();
            LightsOut();
        }
    }

    public void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;

    }

    public void LightsOut()
    {
        foreach (Light light in Lights)
        {
            light.intensity = 0;
        }

        foreach (GameObject spot in SpotsGeants)
        {
            spot.GetComponent<Renderer>().material.SetColor("_EmissiveColor", new Color(0, 0, 0, 0));
        }

    }
}
