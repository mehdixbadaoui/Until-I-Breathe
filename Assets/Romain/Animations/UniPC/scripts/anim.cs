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

    public bool isCrouching = false;


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
        Debug.Log("MyAniConScript: start => Animator");

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


        myAnimator.SetBool("crouch", isCrouching);

        // Check the ground a little time after the jump
        if (Movement.isGrounded && movement.countGround > 5)
        {
            myAnimator.SetBool("isGrounded", true);
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
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

}