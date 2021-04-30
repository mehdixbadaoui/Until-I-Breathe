using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnceintePush : MonoBehaviour
{
    public GameObject boxToPush;
    public LeverEnceinte lever;

    private LineRenderer LR;

    //Just add a line between the start and end position for testing purposes
    public Vector3[] positions = new Vector3[2];

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponentInParent<LineRenderer>();

        positions[0] = transform.parent.transform.position;
        positions[1] = transform.parent.parent.GetComponentInChildren<SpringJoint>().transform.position;
        //Add the positions to the line renderer
        LR.positionCount = positions.Length;
        LR.SetPositions(positions);

    }

    // Update is called once per frame
    void Update()
    {

        positions[0] = transform.parent.transform.position; //new Vector3(transform.parent.GetComponent<Collider>().bounds.center.x , transform.parent.GetComponent<Collider>().bounds.max.y , transform.parent.position.z );
        //Add the positions to the line renderer
        LR.positionCount = positions.Length;
        LR.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "box")
        {
            boxToPush.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-1000));
            Debug.Log("AddForce");
            if(lever)
            {
                lever.isOn = true;
            }
        }
    }
}
