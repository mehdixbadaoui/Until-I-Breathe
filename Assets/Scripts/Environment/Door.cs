using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private int moveDoor = 0;
    private int angleDoor = 0;
    private bool open = false;
    private bool close = false;

    public Transform doorPivot;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveDoor != 0 
            && ((open && Math.Abs(angleDoor + moveDoor) <= 90) || ( close && angleDoor != 0 )) )
        {
            angleDoor += moveDoor * 10;
            doorPivot.Rotate(0, moveDoor * 10, 0);
        }
        else
        {
            open = false;
            close = false;
            moveDoor = 0;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {

        if(other.tag == "uni"){
            open = true;
            close = false;
            this.GetComponent<BoxCollider>().size = new Vector3(this.GetComponent<BoxCollider>().size.x, this.GetComponent<BoxCollider>().size.y, 1.1f);

            if (other.transform.position.z < this.transform.position.z){
                moveDoor = -1 ;
                //transform.parent.Rotate(0,-90,0);
            }

            else if(other.transform.position.z > this.transform.position.z)
            {
                moveDoor = 1 ;
                //transform.parent.Rotate(0,90,0);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {

        if(other.tag == "uni"){

            close = true;
            open = false;
            this.GetComponent<BoxCollider>().size = new Vector3(this.GetComponent<BoxCollider>().size.x, this.GetComponent<BoxCollider>().size.y, 1f);

            if (angleDoor > 0)
            {
                moveDoor = -1 ;
                //transform.parent.Rotate(0,-90,0);
            }

            else if ( angleDoor < 0 )
            {
                moveDoor = +1;
                //transform.parent.Rotate(0,90,0);
            }

            //transform.parent.rotation = new Quaternion(0,0,0,1);

        }
    }

}
