using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardGaz : MonoBehaviour
{
    public bool CanHold = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            if(!other.GetComponent<Breathing_mechanic>().hold || !CanHold)
            {
                GetComponentInParent<CanBreathe>().enabled = false;
                other.GetComponent<Breathing_mechanic>().can_breath = false;
                //other.GetComponent<Breathing_mechanic>().breath_speed = initial_breath_speed * 10f;
                if(other.GetComponent<Breathing_mechanic>().breath > 0.1)
                    other.GetComponent<Breathing_mechanic>().breath = 0.1f;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            GetComponentInParent<CanBreathe>().enabled = true;
            //other.GetComponent<Breathing_mechanic>().breath_speed = initial_breath_speed;
        }

    }
}
