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

    // Rigidbody
    private Rigidbody rig;

    //Previous constrains of the box
    private RigidbodyConstraints previousContraints;

    private Inputs inputs;

   


    private void Awake()
    {
        inputs = new Inputs();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }

    void Start()
    {
        
        //Get Movement component
        movements = GetComponent<Movement>();

        // Get the collider of the player
        col = GetComponent<Collider>();

        // Get the Rigidbody of the player
        rig = GetComponent<Rigidbody>();
      
        
    }

    void Update()
    {
        CheckForLedge();

        if (grabbing)
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y , transform.position.z + distToBox.z) ;
            //box.GetComponent<Rigidbody>().velocity = rig.velocity;
    }

    protected virtual void CheckForLedge()
    {
        handsOfPlayer = new Vector3(transform.position.x, col.bounds.min.y + (col.bounds.max.y - col.bounds.min.y)/2 + offsetGrabbing, transform.position.z);
        RaycastHit hit;
        RaycastHit hitSecurity;
        if ((Movement.isGrounded && !Movement.isGrapplin) && Physics.Raycast(handsOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, grabbingDistance))
        {
            if (!hit.collider.isTrigger && hit.collider.gameObject.tag == "box" && inputs.Uni.Move_Box.ReadValue<float>() > 0)
            {
                box = hit.collider.gameObject;
                distToBox = box.transform.position - transform.position;
                grabbing = true;
                Movement.isGrabbing = true;
                previousContraints = box.GetComponent<Rigidbody>().constraints;
                box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                box.GetComponent<Rigidbody>().isKinematic = false;
                

            }

        }
        if (grabbing && (inputs.Uni.Move_Box.ReadValue<float>() == 0 || !Movement.isGrounded))
        {
            
            grabbing = false;
            Movement.isGrabbing = false;
            box.GetComponent<Rigidbody>().constraints = previousContraints;
            box.GetComponent<Rigidbody>().isKinematic = true;
            
            
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        //Gizmos.DrawLine(handsOfPlayer, handsOfPlayer + transform.TransformDirection(new Vector3(0, 0, grabbingDistance) * transform.localScale.z));
        if(box!=null)
            Gizmos.DrawLine(box.transform.position, transform.position + distToBox + new Vector3(0, 0, distToBox.z * 0.1f));
    }
}
