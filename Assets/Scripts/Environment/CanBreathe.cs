using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBreathe : MonoBehaviour
{
    private Breathing_mechanic breathing_mechanic;

    // Start is called before the first frame update
    void Start()
    {
        breathing_mechanic = FindObjectOfType<Breathing_mechanic>();

    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("uni") && enabled)
        {
            //DISABLE AIR LOSS
            breathing_mechanic.breath = breathing_mechanic.max_breath;
            breathing_mechanic.can_breath = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("uni") && enabled)
        {
            breathing_mechanic.can_breath = false;
            breathing_mechanic.hold = true;
        }
    }


}
