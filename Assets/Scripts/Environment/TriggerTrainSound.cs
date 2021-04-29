using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrainSound : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni")
        {
            AkSoundEngine.PostEvent("Break_UntilIBreathe_Level2_3_Run_Loop_event", GameObject.FindGameObjectWithTag("WwiseSound"));
        }
    }
}
