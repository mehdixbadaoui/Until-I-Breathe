 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    private Inputs inputs;

    private Animator myAnimator;

    // Local Direction of the player
    private bool localFacingLeft;

    public float vert;

    private Movement movement;
    private Rigidbody rb;

    public bool isCrouching = false;


    public bool pull = false;
    public bool push = false;


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

    void Start () 
    {

        // Get the animator
        myAnimator = GetComponent<Animator>();

        // Get the movement script
        rb = GetComponentInParent<Rigidbody>();

        // Get the movement script
        movement = GetComponentInParent<Movement>();

        // initiate the bool for 180 turn
        localFacingLeft = movement.isFacingLeft;

    }
	
	// Update is called once per frame
	void Update () 
    {
        vert = Input.GetAxis("Horizontal");
        // vert = inputs.Uni.Walk.ReadValue<float>();:

        myAnimator.SetFloat("vertical", Mathf.Abs(vert));
        // Debug.Log("vertical = " + Mathf.Abs(Input.GetAxis("Vertical")));

        // random int for 2 different falls
        myAnimator.SetInteger("randomFall", Random.Range(0, 2));

        myAnimator.SetBool("grapplinPush", movement.animPushing);

        myAnimator.SetBool("grapplinIdle", movement.animIdleAir);

        myAnimator.SetBool("crouch", isCrouching);

        myAnimator.SetBool("canWalk", !movement.hit);

        // Check the ground a little time after the jump
        if (Movement.isGrounded && movement.countGround > 5)
        {
            myAnimator.SetBool("isGrounded", true);
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
        }

        if(Movement.isGrabbing)
        {
            // il push si il s'accroche juste sans bouger
            if( !myAnimator.GetBool("pull") && !myAnimator.GetBool("push") && vert == 0)
            {
                myAnimator.SetBool("pull", false);
                myAnimator.SetBool("push", true);
            }

            // il push si son forward est dans le meme sens que son mouvement
            if (vert * rb.transform.localScale.z > 0)
            {
                myAnimator.SetBool("pull", false);
                myAnimator.SetBool("push", true);
            }

            // il pull si son forward est dans le sens contraire que son mouvement
            if (vert * rb.transform.localScale.z < 0)
            {
                myAnimator.SetBool("pull", true);
                myAnimator.SetBool("push", false);
            }
        }
        else
        {
            myAnimator.SetBool("pull", false);
            myAnimator.SetBool("push", false);
        }


        //myAnimator.SetBool("isGrounded", Movement.isGrounded);
        // Debug.Log("vertical = " + Mathf.Abs(Input.GetAxis("Vertical")));

        if (localFacingLeft != movement.isFacingLeft)
        {
            localFacingLeft = movement.isFacingLeft;
            myAnimator.SetBool("180", true);
        }
        else
        {
            myAnimator.SetBool("180", false);
        }

    }
    void OnDrawGizmos()
    {
/*
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rb.transform.position, rb.transform.position + rb.gameObject.transform.TransformDirection(new Vector3(0, 0, 1) * rb.transform.localScale.z));*/
    }

}