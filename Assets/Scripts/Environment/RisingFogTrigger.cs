using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFogTrigger : MonoBehaviour
{
    public RisingFog Fog;
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
            Fog.RiseSpeed = speed;
    }

}
