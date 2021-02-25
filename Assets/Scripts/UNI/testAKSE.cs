using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAKSE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AkSoundEngine.PostEvent("FS_Play", gameObject);
        }
    }
}
