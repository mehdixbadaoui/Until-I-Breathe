using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    //Movement script
    private Movement movements;

    // Hands of player
    private Vector3 handsOfPlayer;

    // Collider of the player
    private Collider col;

    // offset for the arms of Uni
    public float offsetGrabbing;

    //distance to grab the box
    public float grabbingDistance = 0.5f;

    // If true, the box is following the player
    private bool grabbing = false;

    // Box to move
    private GameObject box;

    // Key to grab the box
    public KeyCode keyGrabbing;

    // Distance to the box
    private Vector3 distToBox;


    void Start()
    {

        //Get Movement component
        movements = GetComponent<Movement>();

        // Get the collider of the player
        col = GetComponent<Collider>();
    }

    void Update()
    {
        CheckForLedge();

        if (grabbing)
            box.transform.position = transform.position + distToBox;
    }

    protected virtual void CheckForLedge()
    {
        handsOfPlayer = new Vector3(transform.position.x, col.bounds.min.y + (col.bounds.max.y - col.bounds.min.y)/2 + offsetGrabbing, transform.position.z);
        RaycastHit hit;
        RaycastHit hitSecurity;
        if ((Movement.isGrounded && !Movement.isGrapplin) && Physics.Raycast(handsOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, grabbingDistance))
        {
            if (!hit.collider.isTrigger && hit.collider.gameObject.tag == "box" && Input.GetKey(keyGrabbing))
            {
                box = hit.collider.gameObject;
                distToBox = box.transform.position - transform.position;
                grabbing = true;
                Debug.Log("grabbing");
                box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            }
            else
            {
                grabbing = false;
                box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }


        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawLine(handsOfPlayer, handsOfPlayer + transform.TransformDirection(new Vector3(0, 0, grabbingDistance) * transform.localScale.z));
    }
}
