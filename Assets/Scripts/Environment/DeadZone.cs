using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    // Game Manager
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        // Game master
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni")
        {
            if (!other.GetComponentInChildren<Ragdoll>().OnOff)
            {
                AkSoundEngine.PostEvent("Uni_Voice_Death_event", GameObject.FindGameObjectWithTag("uni"));
                gm.Die();

            }
            
        }
    }
}
