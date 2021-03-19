using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardGaz : MonoBehaviour
{
    private float initial_breath_speed;
    // Start is called before the first frame update
    void Start()
    {
        initial_breath_speed = FindObjectOfType<Breathing_mechanic>().breath_speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni") && !other.GetComponent<Breathing_mechanic>().hold)
        {
            GetComponentInParent<CanBreathe>().enabled = false;
            other.GetComponent<Breathing_mechanic>().can_breath = false;
            //other.GetComponent<Breathing_mechanic>().breath_speed = initial_breath_speed * 10f;
            if(other.GetComponent<Breathing_mechanic>().breath > 1)
                other.GetComponent<Breathing_mechanic>().breath = 1;
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
