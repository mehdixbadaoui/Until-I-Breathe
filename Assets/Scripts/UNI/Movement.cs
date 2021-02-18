﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public float speed = .2f;
    public float grapplinSpeed = 1;
    float horizontal_movement;

    public float jump_force = .5f;
    public static bool isGrounded = false;
    public static bool isGrapplin = false;
    public static float distToHook;

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
    public bool on_slope;
    Vector3 slope_norm;
    public bool too_steep;

    private Vector3 lastVelocity;
    public float horizontalVelocityMax = 5;

    private int countGround = 0;

    private bool collisionWithWall; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        facingLeft = new Vector3(1, transform.localScale.y, -transform.localScale.z);

        capsule_collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_movement = Input.GetAxis("Horizontal");
        vertical_movement = Input.GetAxisRaw("Vertical");


        //horizontal_movement = Input.GetAxisRaw("Horizontal");
 

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && countGround > 5)
        {
            Jump();
        }


        if (Input.GetKey(KeyCode.S))
            GetComponent<CapsuleCollider>().height = 1;
        else
            GetComponent<CapsuleCollider>().height = 1.5f;


    }

    private void FixedUpdate()
    {
        check_ground();
        countGround += 1;
        SlopeCheck();

        if (on_slope)
        {
            if (too_steep)
                rb.drag = 0f;
            else if (isGrounded)
                rb.drag = 10f;
            else
                rb.drag = .2f;
        }
        else
            rb.drag = .2f;

        if (isGrounded && !isGrapplin && countGround > 5 /*|| lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized*/)
        {
            transform.Translate(new Vector3(0f, -Convert.ToInt32(on_slope && horizontal_movement != 0) * slopeforce, Convert.ToInt32(!too_steep) * horizontal_movement * speed));
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


        if (isJumping || (!isGrounded && !isGrapplin && !isJumpingAftergrapplin) )
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

        //if(collisionWithWall)
        //{
        //    rb.velocity = new Vector3(0f, 0f, 0f);
        //}

            
        
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

        //isGrounded = Physics.BoxCast(capsule_collider.bounds.center, transform.localScale / 2, Vector3.down, out ground_hit, Quaternion.identity, extra_height);

    }

    void SlopeCheck()
    {
        RaycastHit slope_ray;
        bool cast = Physics.Raycast(capsule_collider.bounds.center, Vector3.down, out slope_ray, capsule_collider.height / 2 + ground_dist);
        on_slope = cast && slope_ray.normal != Vector3.up;

        too_steep = on_slope && Mathf.Abs(slope_ray.collider.transform.rotation.x) >= .3f;
    

        //IDK WHAT THIS DOUBLE IF DOES BUT IF YOU DELETE IT JUMPING IS WEIRD
        if (cast && on_slope)
        {
            if (slope_norm != Vector3.up)
            {

                //transform.Translate(new Vector3(0f, -slopeforce, .1f) * speed);
                //if (Mathf.Abs(slope_ray.transform.rotation.x) >= .3f)//too steep

            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(capsule_collider.bounds.center + transform.forward * 0f, .1f);
    }

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
