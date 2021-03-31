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
    public bool open = false;
    public bool close = false;
    public bool locked = false;

    public Transform doorPivot;
    public GameObject meshDoor;
    private DistanceUniFromObjects distanceUniFromObjects;
    private GameObject uni;
    public float sizeTriggerIfEnter = 1.1f;
    public float sizeTriggerIfExit = 1f;
    public Vector3 dstDoorUni;
    public float maxDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        uni = GameObject.FindGameObjectWithTag("uni");
        distanceUniFromObjects = uni.GetComponent<DistanceUniFromObjects>();
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
        dstDoorUni = distanceUniFromObjects.CalculateDistanceUniFromObject(this.gameObject.transform.position.z); 
        if (this.tag == "slidingDoor")
        {
            
            if ( open && Math.Abs(slideDoor) < lengthDoor )
            {
                slideDoor -= 0.1f ;
                doorPivot.Translate(-0.1f, 0, 0);
                distanceUniFromObjects.RTPCGameObjectValue(dstDoorUni, maxDistance, this.gameObject, "Porte_coulissante_ouverte_event", "DoorVolume"); 
                
            }
            else if (close && slideDoor < 0)
            {
                slideDoor += 0.1f;
                doorPivot.Translate(0.1f, 0, 0);
                distanceUniFromObjects.RTPCGameObjectValue(dstDoorUni, maxDistance, this.gameObject, "Porte_coulissante_fermee_event", "DoorVolume");
                

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
                if (this.tag == "Door_Wood")
                {
                    if(open)
                        distanceUniFromObjects.RTPCGameObjectValue(dstDoorUni, maxDistance, this.gameObject, "Porte_ouverte_bois_event", "DoorVolume");
                    else if(close)
                        distanceUniFromObjects.RTPCGameObjectValue(dstDoorUni, maxDistance, this.gameObject, "Porte_fermee_bois_event", "DoorVolume");
                }
                else if (this.tag == "Door_Metal")
                {
                    if(open)
                        distanceUniFromObjects.RTPCGameObjectValue(dstDoorUni, maxDistance, this.gameObject, "Porte_ouverte_metal_event", "DoorVolume");
                    else if(close)
                        distanceUniFromObjects.RTPCGameObjectValue(dstDoorUni, maxDistance, this.gameObject, "Porte_fermee_metal_event", "DoorVolume");
                }
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
            this.GetComponent<BoxCollider>().size = new Vector3(this.GetComponent<BoxCollider>().size.x, this.GetComponent<BoxCollider>().size.y, sizeTriggerIfEnter);

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
            this.GetComponent<BoxCollider>().size = new Vector3(this.GetComponent<BoxCollider>().size.x, this.GetComponent<BoxCollider>().size.y, sizeTriggerIfExit);

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
