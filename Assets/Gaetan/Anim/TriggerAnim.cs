using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    public bool inside = false;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            inside = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
