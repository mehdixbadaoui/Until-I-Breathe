using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    private Inputs inputs;

    public Animator myAnimator;

    private Breathing_mechanic breathing;

    public float speed = .2f;
    private float previousSpeed ;

    public float grapplinSpeed = 1;
    public float horizontal_movement;

    //Grapplin 
    [HideInInspector] public bool animPushing;
    [HideInInspector] public bool animIdleAir;

    public string typeOfGround = ""; 
    // Jump and slope
    /*[HideInInspector]*/
    public float jump_force = .5f;
    public float jump_force_flat = .5f;
    public float jump_force_slope_up = .5f;
    public float jump_force_slope_down = .5f;
    public static float distToHook; 
    public static float angleHook;
    public float velocityMaxJump = 3;
    public float horizontalVelocityMax = 10;

    private bool isFlying = false;

    private bool isCrouching = false; 

    float vertical_movement;
    private Vector3 lastInput;
    private Vector3 lastInputJumping;
    private Vector3 lastVelocityJumping;

    public Vector3 direction;

    Rigidbody rb;
    //[HideInInspector]

    // Bools 
    public bool isGroundedVerif;
    RaycastHit middle;
    public static bool canMove = true;
    public static bool isGrounded = false;
    public static bool isGrapplin = false;
    public static bool isGrabbing = false;
    public static bool isJumping = false;
    public bool isFacingLeft;
    private bool isJumpingAftergrapplin;
    public bool on_slope_up;
    public bool on_slope_down;
    private bool collisionWithWall;
    public bool too_steep;
    private bool canJump;

    private Vector3 facingLeft;

    RaycastHit ground_hit;
    RaycastHit hit_front;

    CapsuleCollider capsule_collider;
    private Vector3 colliderSize;
    public float ground_dist = .3f;

    public float wall_detector_dist = .1f;
    public float slopeforce;
    [SerializeField] private float slopeforce_val;
    Vector3 slope_norm;
    public float slope_check_dist;

    private Vector3 lastVelocity;

    public int countGround = 0;
    public int countNotGround = 0;


    private GameMaster gm;

    private LedgeLocator ledge_locator;

    public bool hit;

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

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        ledge_locator = FindObjectOfType<LedgeLocator>();

        capsule_collider = GetComponent<CapsuleCollider>();

        // Get the animator 
        myAnimator = GetComponentInChildren<Animator>();

        // Get the breathing script
        breathing = GetComponent<Breathing_mechanic>();

        //INPUTS
        //inputs.Uni.Jump.performed += ctx => Jump();
        //inputs.Uni.Crouch.performed += ctx => Crouch();
        inputs.Uni.Die.performed += ctx => gm.Die();
    }


    // Update is called once per frame
    void Update()
    {
        //horizontal_movement = inputs.Uni.Walk.ReadValue<float>();
        //Debug.Log(inputs.Uni.Walk.ReadValue<float>());
        horizontal_movement = Input.GetAxis("Horizontal");

        if (canMove)
        {
            //JUMPING
            if (inputs.Uni.Jump.ReadValue<float>() == 1 && isGrounded && countGround > 5 && canJump)
            {
                canJump = false;
                Jump();
            }
            else if (inputs.Uni.Jump.ReadValue<float>() == 0)
            {
                canJump = true;
            }

            //CROUCHING
            if (Convert.ToBoolean(inputs.Uni.Crouch.ReadValue<float>()) && !isGrapplin && !isGrabbing)
            {
                if (capsule_collider.height > 1)
                {
                    myAnimator.GetComponent<anim>().isCrouching = true;
                    myAnimator.Play("stand2crouch", 1);
                    myAnimator.Play("stand2crouch", 2);
                    capsule_collider.center = new Vector3(capsule_collider.center.x, capsule_collider.center.y - (capsule_collider.height - 1) / 2, capsule_collider.center.z);
                    capsule_collider.height = 1;
                    previousSpeed = speed;
                    speed = speed * 0.5f;
                }

            }
            else
            {
                if (capsule_collider.height == 1)
                {
                    Vector3 topOfPlayer = new Vector3(transform.position.x, capsule_collider.bounds.max.y - 0.01f, transform.position.z);
                    RaycastHit hit_top;
                    if (!Physics.Raycast(topOfPlayer, transform.TransformDirection(Vector3.up * transform.localScale.y), out hit_top, 0.51f))
                    {
                        capsule_collider.center = new Vector3(capsule_collider.center.x, capsule_collider.center.y + 0.25f, capsule_collider.center.z);
                        capsule_collider.height = 1.5f;
                        speed = previousSpeed;
                        myAnimator.GetComponent<anim>().isCrouching = false;
                        myAnimator.Play("crouch2stand", 1);
                        myAnimator.Play("crouch2stand", 2);
                    }
                }

            }

            isGroundedVerif = isGrounded;

            if (isGrapplin && !isGrounded && countGround > 10)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(angleHook, 0, 0), .1f);
            }
            else if (!isGrounded)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .1f);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .01f);
            }
            /*(middle.transform !=  null)*/
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;

    //     ////Check if there has been a hit yet
    //     //if (hitt)
    //     //{
    //     //    Gizmos.DrawSphere(hit.point, 1);
    //     //    //Draw a Ray forward from GameObject toward the hit
    //     //    Gizmos.DrawRay(capsule_collider.bounds.center, transform.TransformDirection(Vector3.forward * transform.localScale.z) * hit.distance);
    //     //    //Draw a cube that extends to where the hit exists
    //     //    Gizmos.DrawWireCube(capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * hit.distance, transform.lossyScale / 2);
    //     //}
    //     ////If there hasn't been a hit yet, draw the ray at the maximum distance
    //     //else
    //     //{
    //     //    //Draw a Ray forward from GameObject toward the maximum distance
    //     //    Gizmos.DrawRay(capsule_collider.bounds.center, transform.TransformDirection(Vector3.forward * transform.localScale.z) * wall_detector_dist);
    //     //    //Draw a cube at the maximum distance
    //     //    Gizmos.DrawWireCube(capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * wall_detector_dist, transform.lossyScale / 2);
    //     //}

    //     //Gizmos.DrawSphere(capsule_collider.bounds.center, .1f);
    // }


    private void FixedUpdate()
    {
        if (canMove)
        {

            check_ground();
            countGround += 1;
            countNotGround += 1;
            if (isGrounded == true)
                countNotGround = 0;
            SlopeCheck();

            hit = Physics.BoxCast(capsule_collider.bounds.center, new Vector3(capsule_collider.radius, (capsule_collider.height / 2) * 0.8f, 0), transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit_front, Quaternion.identity, capsule_collider.radius + 0.01f);
            //hit = false;
            if (hit)
            {
                if (hit_front.collider.isTrigger || hit_front.transform.tag == "box")
                {
                    hit = false;
                }
            }

            #region Slope Behaviour
            if (on_slope_up || on_slope_down)
            {
                if (on_slope_up)
                {
                    jump_force = jump_force_slope_up;
                }
                else if (on_slope_down)
                {
                    jump_force = jump_force_slope_down;

                }
                if (too_steep)
                    rb.drag = 0f;
                else if (isGrounded)
                    rb.drag = 0.2f;
                else
                    rb.drag = .2f;
            }
            else
            {
                rb.drag = .2f;
                jump_force = jump_force_flat;
            }
            #endregion

            #region passage sur ventilo

            // Lors du passage sur le vent d'un ventilateur
            if (isFlying && !isGrapplin)
            {

                isGrounded = false;
                isJumping = false;
                isJumpingAftergrapplin = false;

                rb.velocity += new Vector3(0, 0, (horizontal_movement * speed * 60 - rb.velocity.z) * 0.2f);
                //transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);

            }
            #endregion

            // Walk on the ground
            if (isGrounded && !isGrapplin && canJump && !hit) //countGround > 5 /*|| lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized*/)
            {

                //transform.Translate(new Vector3(0f, -Convert.ToInt32(on_slope_down && horizontal_movement != 0) * slopeforce, Convert.ToInt32(!too_steep) * horizontal_movement * speed));
                rb.velocity = new Vector3(0f, rb.velocity.y - Convert.ToInt32(on_slope_down && horizontal_movement != 0) * slopeforce, Convert.ToInt32(!too_steep) * horizontal_movement * speed * 60);

                isJumping = false;
                isJumpingAftergrapplin = false;

                lastInputJumping = Vector3.zero;

            }

            // Walk on the ground
            if (isGrounded && isGrapplin && !hit/*|| lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized*/)
            {

                //transform.Translate(new Vector3(0f, -Convert.ToInt32(on_slope_down && horizontal_movement != 0) * slopeforce, Convert.ToInt32(!too_steep) * horizontal_movement * speed));
                //rb.velocity = new Vector3(0f, -Convert.ToInt32(on_slope_down && horizontal_movement != 0) * slopeforce, Convert.ToInt32(!too_steep) * horizontal_movement * speed * 30);
                rb.velocity = new Vector3(0f, rb.velocity.y - Convert.ToInt32(on_slope_down && horizontal_movement != 0) * slopeforce, Convert.ToInt32(!too_steep) * horizontal_movement * speed * 60);

                isJumping = false;
                isJumpingAftergrapplin = false;
                //lastInputJumping = new Vector3(0f, 0f, horizontal_movement);

                lastInputJumping = Vector3.zero;
            }


            // Add force if isgrapplin because Translate isnt workinbg with spring joint
            else if (isGrapplin && !isGrounded)
            {

                isJumping = false;
                isJumpingAftergrapplin = false;

                Vector3 acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;


                //If we are not too fast and if we are not upper than the hook we can apply a force
                if (distToHook > 0.3f && Math.Abs(rb.velocity.z) < 10f)
                {
                    //animPushing = false;

                    //if we are at the bottom of the rope without moving
                    if (distToHook > 0.95f && Math.Abs(rb.velocity.z) < 1)
                    {
                        animIdleAir = true;
                        rb.AddForce(new Vector3(0f, 0f, horizontal_movement) * grapplinSpeed, ForceMode.Impulse);
                    }
                    //if we are pushing in the same directiont than the swing
                    else if (rb.velocity.z * horizontal_movement >= 0 && rb.velocity.y <= 0)
                    {
                        if (horizontal_movement != 0)
                        {
                            animIdleAir = false;
                            animPushing = false;
                        }

                        if (Math.Abs(rb.velocity.z) >= 1)
                        {
                            rb.AddForce(new Vector3(0f, 0f, horizontal_movement) * distToHook * grapplinSpeed * (1 / Math.Abs(rb.velocity.z)), ForceMode.VelocityChange);
                        }
                        else
                        {
                            rb.AddForce(new Vector3(0f, 0f, horizontal_movement) * grapplinSpeed, ForceMode.VelocityChange);
                        }
                    }
                    //If we are pushing the movement of the swing we slow the movement
                    else if (rb.velocity.z * horizontal_movement < 0)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * 0.99f);
                    }
                }
                // Si on est trop a la fin du mouvement on lance l'animation du push
                if (rb.velocity.y > 0 && acceleration.y < 0.5 && rb.velocity.z * transform.TransformDirection(Vector3.forward * transform.localScale.z).z >= 0)
                {
                    animIdleAir = false;
                    animPushing = true;
                }

            }

            // // Lors d'un saut depuis le sol
            // if (isJumping || (!isGrounded && !isGrapplin && !isJumpingAftergrapplin && !isFlying))
            // {r

            //     // Si on pousse dans lesens contraire dans les airs, on rejoint la vélocité d'avant le saut en négatif (ou speed*60 si elle etait trop faible)
            //     if (lastInputJumping.normalized != new Vector3(0f, 0f, horizontal_movement).normalized && lastVelocityJumping.z != 0 && lastInputJumping.z != 0 && horizontal_movement != 0)

            //Code for Jumping
            if (isJumping && (!hit || horizontal_movement != transform.TransformDirection(Vector3.forward * transform.localScale.z).z))
            {


                /*if (lastInputJumping == Vector3.zero)
                {
                    Debug.Log("lastInputJumping changed");
                    lastInputJumping = new Vector3(0f, 0f, horizontal_movement);
                    lastVelocityJumping = rb.velocity;
                }*/

                // Si on pousse dans le sens contraire dans les airs, on rejoint la vélocité d'avant le saut en négatif (ou speed*60 si elle etait trop faible)
                if (/*lastInputJumping.normalized != new Vector3(0f, 0f, horizontal_movement).normalized*/ (lastInputJumping.z * horizontal_movement < 0) && lastVelocityJumping.z != 0 && Math.Abs(lastInputJumping.z) > 0.5f && Math.Abs(horizontal_movement) == 1)
                {
                    //transform.Translate(new Vector3(0f, 0f, horizontal_movement / 2.5f) * speed);
                    rb.velocity += new Vector3(0, 0, (Math.Max(speed * 60, Math.Abs(lastVelocityJumping.z)) * -(lastVelocityJumping.z / Math.Abs(lastVelocityJumping.z)) - rb.velocity.z) * 0.1f);

                }
                // Si on pousse dans le même sens que la direction dans les airs, on rejoint la vélocité d'avant le saut (ou speed*60 si elle etait trop faible)
                else if (/*lastInputJumping.normalized == new Vector3(0f, 0f, horizontal_movement).normalized*/ (lastInputJumping.z * horizontal_movement > 0) && lastVelocityJumping.z != 0 && Math.Abs(lastInputJumping.z) > 0.5f)
                {
                    rb.velocity += new Vector3(0, 0, (Math.Min(speed * 60, Math.Abs(lastVelocityJumping.z)) * (lastVelocityJumping.z / Math.Abs(lastVelocityJumping.z)) - rb.velocity.z) * 0.5f);
                }

                else if (lastVelocityJumping.z == 0 || Math.Abs(lastInputJumping.z) <= 0.5f)
                {
                    rb.velocity += new Vector3(0, 0, (horizontal_movement * speed * 60 - rb.velocity.z) * 0.2f);
                }
            }
            // Si on saute sur un mur la velocite en y reste la même mais pas celle en z
            else if (hit)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            // Si on tombe juste d'une plateforme
            else if (!isGrounded && !isGrapplin && !isJumpingAftergrapplin && !isFlying && (!hit || horizontal_movement != transform.TransformDirection(Vector3.forward * transform.localScale.z).z))
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, horizontal_movement * Math.Min(speed * 60, Math.Abs(rb.velocity.z + horizontal_movement)));
            }

            // Lors d'un saut apres grappin
            if (isJumpingAftergrapplin && rb.velocity.z * horizontal_movement < 0)
            {
                transform.Translate(new Vector3(0f, 0f, horizontal_movement / 2.5f) * speed);
            }

            else if (isJumpingAftergrapplin)
            {
                rb.AddForce(new Vector3(0f, 0f, rb.velocity.z * 0.8f) * speed);
            }

            //Checking if we need to flip our character
            if (horizontal_movement != 0 && !isGrabbing)
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
        }
    }

    void Jump()
    {
        
        if (!breathing.hold)
            myAnimator.Play("Unijump" , 1);
        myAnimator.Play("Unijump", 2);
        countGround = 0;
        isGrounded = false;
        isJumping = true;
        lastInputJumping = new Vector3(0f, 0f, horizontal_movement);
        lastVelocityJumping = rb.velocity;
        rb.transform.Translate(new Vector3(0 , 0.01f , 0));
        rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
        
    }

    public void JumpAfterGrapplin()
    {

        if (!breathing.hold)
            myAnimator.Play("JumpAfterGrapplin" , 1);
        myAnimator.Play("JumpAfterGrapplin", 2);

        countGround = 0;
        isGrounded = false;
        isJumpingAftergrapplin = true;

        
        if (rb.velocity.z > horizontalVelocityMax)
            rb.velocity = new Vector3(0, rb.velocity.y, horizontalVelocityMax);
        if (rb.velocity.z < -horizontalVelocityMax)
            rb.velocity = new Vector3(0, rb.velocity.y, -horizontalVelocityMax);
        // Debug.Log(rb.velocity.z);
        
        if (rb.velocity.y> velocityMaxJump)
            rb.velocity = new Vector3(rb.velocity.x, velocityMaxJump, rb.velocity.z);

        lastInputJumping = new Vector3(0f, 0f, rb.velocity.z);
    }

    
    protected virtual void Flip()
    {
        if (isGrapplin)
        {
            animIdleAir = true;
            animPushing = false;
        }
        

        if (isFacingLeft && !isGrabbing)
        {
            transform.localScale = facingLeft;
        }
        if (!isFacingLeft && !isGrabbing)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, -transform.localScale.z);
        }
    }

    void check_ground()
    {
        RaycastHit front;
        //RaycastHit middle;
        RaycastHit back;
        bool frontRaycast = Physics.Raycast(capsule_collider.bounds.center + transform.forward * .2f, Vector3.down, out front, capsule_collider.height / 2 + ground_dist); 
        bool backRaycast = Physics.Raycast(capsule_collider.bounds.center - transform.forward * .2f, Vector3.down, out back, capsule_collider.height / 2 + ground_dist);
        bool middleRaycast = Physics.Raycast(capsule_collider.bounds.center, Vector3.down, out middle, capsule_collider.height / 2 + ground_dist); 

        isGrounded = (frontRaycast || middleRaycast || backRaycast);
        if(isGrounded)
        {
            if (middleRaycast)
                typeOfGround = middle.collider.gameObject.tag;
            else if (frontRaycast)
                typeOfGround = front.collider.gameObject.tag;
            else if (backRaycast)
                typeOfGround = back.collider.gameObject.tag;
        }
        
    
        

    }

    void SlopeCheck()
    {

        RaycastHit slope_ray_front;
        RaycastHit slope_ray_back;

        bool cast_front = Physics.Raycast(capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, Vector3.down, out slope_ray_front, capsule_collider.height / 2 + slope_check_dist);
        bool cast_back = Physics.Raycast(capsule_collider.bounds.center - transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, Vector3.down, out slope_ray_back, capsule_collider.height / 2 + slope_check_dist);

        on_slope_up = cast_front && slope_ray_front.normal != Vector3.up;
        on_slope_down = cast_back && slope_ray_back.normal != Vector3.up;

        too_steep = (on_slope_up && Mathf.Abs(slope_ray_front.collider.transform.rotation.x) >= .3f) || (on_slope_down && Mathf.Abs(slope_ray_back.collider.transform.rotation.x) >= .3f);


        //IDK WHAT THIS DOUBLE IF DOES BUT IF YOU DELETE IT JUMPING IS WEIRD
        if ((cast_front && on_slope_up) || (cast_back && on_slope_down))
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
        /*
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, capsule_collider.bounds.center + transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f + Vector3.down * (capsule_collider.height / 2 + slope_check_dist));
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(capsule_collider.bounds.center - transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f, capsule_collider.bounds.center - transform.TransformDirection(Vector3.forward * transform.localScale.z) * .1f + Vector3.down * (capsule_collider.height / 2 + slope_check_dist));
        */

        /*      // For grabbing box
                Gizmos.color = Color.yellow;
                Vector3 topOfPlayer = new Vector3(transform.position.x, capsule_collider.bounds.max.y, transform.position.z);
                Gizmos.DrawLine(topOfPlayer, topOfPlayer + transform.TransformDirection(new Vector3(0, 0.5f, 0) * transform.localScale.y));
        */

        /*
        Gizmos.color = Color.yellow;
        if (capsule_collider!= null)
        Gizmos.DrawLine(capsule_collider.bounds.center, capsule_collider.bounds.center - transform.TransformDirection(new Vector3(0, capsule_collider.height / 2 + ground_dist, 0) * transform.localScale.y));*/

/*
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rb.transform.position, rb.transform.position + transform.TransformDirection(new Vector3(0, 0, 1) * transform.localScale.z) ) ;*/
    }

    

}
