using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
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

        if(other.tag == "uni"){
            if(other.transform.position.z < this.transform.position.z){
                transform.parent.Rotate(0,-90,0);
            }

            else if(other.transform.position.z > this.transform.position.z)
            {
                transform.parent.Rotate(0,90,0);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {

        if(other.tag == "uni"){

            transform.parent.rotation = new Quaternion(0,0,0,1);
        }
    }

}
