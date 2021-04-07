using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnceintePush : MonoBehaviour
{
    public GameObject boxToPush;
    public Lever lever;

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
        if (other.tag == "box")
        {
            boxToPush.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-1000));
            Debug.Log("AddForce");
            if(lever)
            {
                //lever.
            }
        }
    }
}
