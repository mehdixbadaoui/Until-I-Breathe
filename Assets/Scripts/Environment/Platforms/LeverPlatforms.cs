using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPlatforms : MonoBehaviour
{
    public GameObject PlatformsWithButtonGO;
    PlatformsWithButton PlatformsWithButtonScript;

    public int platformPos;

    private void Start()
    {
        PlatformsWithButtonScript = PlatformsWithButtonGO.GetComponent<PlatformsWithButton>();
    }

    public void GoTo()
    {
        if (PlatformsWithButtonScript.readyToGo)
        {
            if (PlatformsWithButtonScript.fourPoints)
            {
                if (platformPos == 1)
                {
                    PlatformsWithButtonScript.intDir = 1;
                }
                else if (platformPos == 2)
                {
                    PlatformsWithButtonScript.intDir = 2;
                }
                else if (platformPos == 3)
                {
                    PlatformsWithButtonScript.intDir = 3;
                }
                else if (platformPos == 4)
                {
                    PlatformsWithButtonScript.intDir = 4;
                }
            }
            else
            {
                if (platformPos == 1)
                {
                    PlatformsWithButtonScript.intDir = 1;
                }
                else if (platformPos == 2)
                {
                    PlatformsWithButtonScript.intDir = 2;
                }
                else if (platformPos == 3)
                {
                    PlatformsWithButtonScript.intDir = 3;
                }
            }
        }
    }
}
