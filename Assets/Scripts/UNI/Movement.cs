using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    private Inputs inputs;

    public float speed = .2f;
    public float grapplinSpeed = 1;
    float horizontal_movement;

    /*[HideInInspector]*/ public float jump_force = .5f;
    public float jump_force_flat = .5f;
    public float jump_force_slope_up = .5f;
    public float jump_force_slope_down = .5f;
    public static bool isGrounded = false;
    public static bool isGrapplin = false;
    public static float distToHook;

    private bool isFlying = false;

    private bool isCrouching = false;

    float vertical_movement;
    private Vector3 lastInput;
    private Vector3 lastInputJumping; 

    public Vector3 direction;

    Rigidbody rb;
    [HideInInspector]
    public bool isFacingLeft;
    private Vector3 facingLeft;
    public bool isJumping;
    private bool isJumpingAftergrapplin;

    RaycastHit ground_hit;
    CapsuleCollider capsule_collider;
    public float ground_dist = .3f;

    public float slopeforce;
    [SerializeField] private float slopeforce_val;
    public bool on_slope_up;
    public bool on_slope_down;
    Vector3 slope_norm;
    public bool too_steep;
    public float slope_check_dist;

    private Vector3 lastVelocity;
    public float horizontalVelocityMax = 5;

    private int countGround = 0;

    private bool collisionWithWall;


    public bool IsFlying 
    {
        get { return isFlying; }   // get method
        set { isFlying = value; }  // set method
    }

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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        facingLeft = new Vector3(1, transform.localScale.y, -transform.localScale.z);

        capsule_collider = GetComponent<CapsuleCollider>();

        //INPUTS
        //inputs.Uni.Jump.performed += ctx => Jump();
        //inputs.Uni.Crouch.performed += ctx => Crouch();
    }


    void Crouch()
    {
        if (!isCrouching)
        {
            capsule_collider.height = 1;
            isCrouching = true;

        }
        else
        {
            capsule_collider.height = 1.5f;
            isCrouching = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_movement = inputs.Uni.Walk.ReadValue<float>();

        //JUMPING
        if (Convert.ToBoolean(inputs.Uni.Jump.ReadValue<float>()) && isGrounded && countGround > 5)
        {
            Jump();
        }

        //CROUCHING
        if (Convert.ToBoolean(inputs.Uni.Crouch.ReadValue<float>()))
        {
            capsule_collider.height = 1;

        }
        else
        {
            capsule_collider.height = 1.5f;

        }
    }

    private void FixedUpdate()
    {
        check_ground();
        countGround += 1;
        SlopeCheck();


        if (on_slope_up || on_slope_down)
        {
            if (on_slope_up)
            {
                jump_force = jump_force_slope_up;
            }
            else if (on_slope_down)
            {
                //slopeforce_val = slopeforce;
                jump_force = jump_force_slope_down;

            }
            if (too_steep)
                rb.drag = 0f;
            else if (isGrounded)
                rb.drag = 100f;
            else
                rb.drag = .2f;
        }
        else
        {
            rb.drag = .2f;
            jump_force = jump_force_flat;
        }

        if (isFlying && !isGrapplin)
        {
            isGrounded = false;
            isJumping = false;
            isJumpingAftergrapplin = false;

            transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);

        }

        if (isGrounded && !isGrapplin && countGround > 5 /*|| lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized*/)
        {
            transform.Translate(new Vector3(0f, -Convert.ToInt32(on_slope_down && horizontal_movement != 0) * slopeforce, Convert.ToInt32(!too_steep) * horizontal_movement * speed));
            isJumping = false;
            isJumpingAftergrapplin = false;
            lastInputJumping = new Vector3(0f, 0f, horizontal_movement); 
        }


        //Add force if isgrapplin because Translate isnt workinbg with spring joint
        if (isGrapplin )
        {
            isJumping = false;
            isJumpingAftergrapplin = false;

            Vector3 acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;

            //If we are not too fast and if we are not upper than the hook we can apply a force
            if (distToHook > 0.3f && Math.Abs(rb.velocity.z) < 10f)
            {
                //if we are at the bottom of the rope without moving
                if (distToHook > 0.95f && Math.Abs(rb.velocity.z) < 1)
                {
                    rb.AddForce(new Vector3(0f, 0f, horizontal_movement) * grapplinSpeed, ForceMode.Impulse);
                }
                //if we are pushing in the same directiont than the swing
                else if (rb.velocity.z * horizontal_movement >= 0 && rb.velocity.y <= 0)
                {
                    if (Math.Abs(rb.velocity.z) >= 1)
                    {
                        rb.AddForce(new Vector3(0f, 0f, horizontal_movement) * distToHook * grapplinSpeed * (1 / Math.Abs(rb.velocity.z)), ForceMode.VelocityChange);
                    }
                    else
                    {
                        rb.AddForce(new Vector3(0f, 0f, horizontal_movement) * grapplinSpeed, ForceMode.VelocityChange);
                    }
                }
                //If we are pushing against the movement of the swing we slow the movement
                else if ( rb.velocity.z * horizontal_movement < 0 )
                {
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * 0.99f );
                }
            }
        }


        if (isJumping || (!isGrounded && !isGrapplin && !isJumpingAftergrapplin && !isFlying) )
        {

            if (lastInputJumping.normalized != new Vector3(0f, 0f, horizontal_movement).normalized /*&& isJumping*/)
            {
                transform.Translate(new Vector3(0f, 0f, horizontal_movement / 2.5f) * speed);


            }
            else if (lastInputJumping.normalized == new Vector3(0f, 0f, horizontal_movement).normalized)
            {
                transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
            }
        }

        if (isJumpingAftergrapplin && rb.velocity.z * horizontal_movement < 0 )
        {
            transform.Translate(new Vector3(0f, 0f, horizontal_movement / 2.5f) * speed);
            //transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
        }

        else if(isJumpingAftergrapplin)
        {
            rb.AddForce( new Vector3(0f, 0f, rb.velocity.z *0.8f) * speed );
        }

        
        if (horizontal_movement != 0)
        {

            if (horizontal_movement > 0 && isFacingLeft)
            {
                isFacingLeft = false;
                Flip();
            }
            if (horizontal_movement < 0 && !isFacingLeft)
            {
                isFacingLeft = true;
                Flip();
            }
        }

        lastVelocity = rb.velocity;

        //Debug.Log(isJumping);



    }

    void Jump()
    {
        countGround = 0;
        isGrounded = false;
        isJumping = true;
        lastInputJumping = new Vector3(0f, 0f, horizontal_movement);
        rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
        
    }

    public void JumpAfterGrapplin()
    {
        countGround = 0;
        isGrounded = false;
        isJumpingAftergrapplin = true;

        // Debug.Log("jump after grapplin");

        // Debug.Log(rb.velocity.z);
        if (rb.velocity.z > horizontalVelocityMax)
            rb.velocity = new Vector3(rb.velocity.x, 0, horizontalVelocityMax);
        if (rb.velocity.z < -horizontalVelocityMax)
            rb.velocity = new Vector3(rb.velocity.x, 0, -horizontalVelocityMax);
        // Debug.Log(rb.velocity.z);

        lastInputJumping = new Vector3(0f, 0f, rb.velocity.z);
        //rb.AddForce(new Vector3(0, jump_force*3, 0), ForceMode.Impulse);

    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider.tag == "ground")
    //        can_jump = true;
    //}
    protected virtual void Flip()
    {
        if (isFacingLeft)
        {
            transform.localScale = facingLeft;
        }
        if (!isFacingLeft)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, -transform.localScale.z);
        }
    }

    void check_ground()
    {
        RaycastHit front;
        RaycastHit middle;
        RaycastHit back;

        isGrounded = (Physics.Raycast(capsule_collider.bounds.center + transform.forward * .2f, Vector3.down, out front, capsule_collider.height / 2 + ground_dist)
                    || Physics.Raycast(capsule_collider.bounds.center - transform.forward * .2f, Vector3.down, out back, capsule_collider.height / 2 + ground_dist)
                    || Physics.Raycast(capsule_collider.bounds.center, Vector3.down, out middle, capsule_collider.height / 2 + ground_dist));


    }

    void SlopeCheck()
    {
        RaycastHit slope_ray_front;
        RaycastHit slope_ray_back;

        bool cast_front = Physics.Raycast(capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, Vector3.down, out slope_ray_front, capsule_collider.height / 2 + slope_check_dist);
        bool cast_back = Physics.Raycast(capsule_collider.bounds.center - transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, Vector3.down, out slope_ray_back, capsule_collider.height / 2 + slope_check_dist);

        on_slope_up = cast_front && slope_ray_front.normal != Vector3.up;
        on_slope_down = cast_back && slope_ray_back.normal != Vector3.up;

        too_steep = (on_slope_up && Mathf.Abs(slope_ray_front.collider.transform.rotation.x) >= .3f) || (on_slope_down &&  Mathf.Abs(slope_ray_back.collider.transform.rotation.x) >= .3f);
    

        //IDK WHAT THIS DOUBLE IF DOES BUT IF YOU DELETE IT JUMPING IS WEIRD
        if ((cast_front && on_slope_up ) || (cast_back && on_slope_down))
        {
            if (slope_norm != Vector3.up)
            {

                //transform.Translate(new Vector3(0f, -slopeforce, .1f) * speed);
                //if (Mathf.Abs(slope_ray.transform.rotation.x) >= .3f)//too steep

            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawLine(capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f + Vector3.down * (capsule_collider.height / 2 + slope_check_dist));
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawLine(capsule_collider.bounds.center - transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, capsule_collider.bounds.center - transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f + Vector3.down * (capsule_collider.height / 2 + slope_check_dist));
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.rigidbody.isKinematic || collision.rigidbody == null)
    //    {
    //        collisionWithWall = true; 
    //    }
    //    else if (!collision.rigidbody.isKinematic)
    //    {
    //        collisionWithWall = false; 
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    collisionWithWall = false;
    //}

}
