using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsLauncher : MonoBehaviour
{
    public GameObject PlatformGO;

    [HideInInspector]
    public bool activate;

    Platforms PlatformScripts;

    // Start is called before the first frame update
    void Start()
    {
        PlatformScripts = PlatformGO.GetComponent<Platforms>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "uni")
        {
            activate = true;
        }
    }
}
