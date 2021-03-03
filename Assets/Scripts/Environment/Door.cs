using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private int moveDoor = 0;
    private int angleDoor = 0;
    private float slideDoor = 0;
    private float lengthDoor;
    private bool open = false;
    private bool close = false;
    public bool locked = false;

    public Transform doorPivot;
    public GameObject meshDoor;

    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "slidingDoor")
        {
            lengthDoor = meshDoor.GetComponent<MeshRenderer>().bounds.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.tag == "slidingDoor")
        {
            if ( open && Math.Abs(slideDoor) < lengthDoor )
            {
                slideDoor -= 0.1f ;
                doorPivot.Translate(-0.1f, 0, 0);
            }
            else if (close && slideDoor < 0)
            {
                slideDoor += 0.1f;
                doorPivot.Translate(0.1f, 0, 0);

            }
            else
            {
                open = false;
                close = false;
            }
        }

        else
        {
            if (moveDoor != 0
                && ((open && Math.Abs(angleDoor + moveDoor) <= 90) || (close && angleDoor != 0)))
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
    }

    private void OnTriggerEnter(Collider other) 
    {

        if(other.tag == "uni"){
            open = true;
            close = false;
            this.GetComponent<BoxCollider>().size = new Vector3(this.GetComponent<BoxCollider>().size.x, this.GetComponent<BoxCollider>().size.y, 1.1f);

            if (this.tag != "slidingDoor" && !locked)
            {
                if (other.transform.position.z < this.transform.position.z)
                {
                    moveDoor = -1;
                    //transform.parent.Rotate(0,-90,0);
                }

                else if (other.transform.position.z > this.transform.position.z)
                {
                    moveDoor = 1;
                    //transform.parent.Rotate(0,90,0);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {

        if(other.tag == "uni"){

            close = true;
            open = false;
            this.GetComponent<BoxCollider>().size = new Vector3(this.GetComponent<BoxCollider>().size.x, this.GetComponent<BoxCollider>().size.y, 1f);

            if (this.tag != "slidingDoor" && !locked)
            {
                if (angleDoor > 0)
                {
                    moveDoor = -1;
                }

                if (angleDoor < 0)
                {
                    moveDoor = +1;
                }
            }

        }
    }

}
