using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = .2f;
    float horizontal_movement;

    public float jump_force = .5f;
    public static bool isGrounded = false;
    public static bool isGrapplin = false;

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
    public float extra_height = .3f;

    private Vector3 lastVelocity;
    public float horizontalVelocityMax = 5;

    private int countGround = 0;



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
 

        if (Input.GetKeyDown(KeyCode.Space) && ( isGrounded ) )
        {
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.Space) && (isGrapplin))
        {
            JumpAfterGrapplin();
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

        if (isGrounded && !isGrapplin && countGround > 5 /*|| lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized*/)
        {
            if (isJumping == true )
                Debug.Log(isJumping);
            transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
            isJumping = false;
            isJumpingAftergrapplin = false;
        }

        if (isGrapplin )
        {
            Vector3 acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
            if (rb.velocity.z < 10f && acceleration.y <= 0 )
            {
                rb.AddForce(new Vector3(0f, 0f, horizontal_movement) * speed * 10, ForceMode.VelocityChange);
                isJumping = false;
                isJumpingAftergrapplin = false;
            }
        }

        if (isJumping)
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
        else if (isJumpingAftergrapplin)
        {
            //transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
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

    void JumpAfterGrapplin()
    {
        countGround = 0;
        isGrounded = false;
        isJumpingAftergrapplin = true;

        Debug.Log("jump after grapplin");

        Debug.Log(rb.velocity.z);
        if (rb.velocity.z > horizontalVelocityMax)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, horizontalVelocityMax);
        if (rb.velocity.z < -horizontalVelocityMax)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -horizontalVelocityMax);
        Debug.Log(rb.velocity.z);

        lastInputJumping = new Vector3(0f, 0f, rb.velocity.z);
        rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);

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

    private void check_ground()
    {

        isGrounded = Physics.BoxCast(capsule_collider.bounds.center, transform.localScale / 2, Vector3.down, out ground_hit, Quaternion.identity, extra_height);
/*        if (isGrounded)
            Debug.Log(ground_hit.collider.name);
        else
            Debug.Log("nothing");*/
    }

}
