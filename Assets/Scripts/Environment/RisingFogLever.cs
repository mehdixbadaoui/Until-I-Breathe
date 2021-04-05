using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFogLever : MonoBehaviour
{
    public RisingFog Fog;

    private void Start()
    {
        Fog.enabled = false;
    }

    public void LaunchGaz()
    {
        Fog.enabled = true;
    }
}
