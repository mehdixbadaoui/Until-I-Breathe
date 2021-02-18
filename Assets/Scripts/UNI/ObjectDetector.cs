using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    SphereCollider sphere_collider;
    // Start is called before the first frame update
    void Start()
    {
        sphere_collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == ("blowable") || col.tag == ("button"))
        {
            FindObjectOfType<Breathing_mechanic>().setBlowObj(col.gameObject);
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == ("blowable") || col.tag == ("button"))
        {
            FindObjectOfType<Breathing_mechanic>().setBlowObj(null);
        }
    }
}
