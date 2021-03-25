using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatforms : MonoBehaviour
{
    public GameObject PlatformsWithButtonGO;
    PlatformsWithButton PlatformsWithButtonScript;

    private void Start()
    {
        PlatformsWithButtonScript = PlatformsWithButtonGO.GetComponent<PlatformsWithButton>();
    }

    public void Increment()
    {
        if (PlatformsWithButtonScript.readyToGo)
        {
            if (PlatformsWithButtonScript.fourPoints)
            {
                if (0 <= PlatformsWithButtonScript.intDir && PlatformsWithButtonScript.intDir <= 3)
                    PlatformsWithButtonScript.intDir++; //Increment
                else
                    PlatformsWithButtonScript.intDir = 1;
            }
            else
            {
                if (0 <= PlatformsWithButtonScript.intDir && PlatformsWithButtonScript.intDir <= 2)
                    PlatformsWithButtonScript.intDir++; //Increment
                else
                    PlatformsWithButtonScript.intDir = 1;
            }
        }
    }
}
