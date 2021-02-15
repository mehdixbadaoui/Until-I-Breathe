using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = .2f;
    float horizontal_movement;

    public float jump_force = .5f;
    public static bool isGrounded = false;

    float vertical_movement;
    private Vector3 lastInput;
    private Vector3 lastInputJumping; 

    public Vector3 direction;

    Rigidbody rb;
    [HideInInspector]
    public bool isFacingLeft;
    private Vector3 facingLeft;
    private bool isJumping ;

    RaycastHit ground_hit;
    CapsuleCollider capsule_collider;
    public float extra_height = .3f;

    public float slopeforce;
    bool on_slope;
    Vector3 slope_norm;



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
 

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isJumping = true; 
        }


        if (Input.GetKey(KeyCode.S))
            GetComponent<CapsuleCollider>().height = 1;
        else
            GetComponent<CapsuleCollider>().height = 1.5f;

        //Debug.Log(isJumping); 

    }

    private void FixedUpdate()
    {
        check_ground();
        SlopeCheck();

        if (isGrounded /*|| lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized*/)
        {
            transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
            isJumping = false;
        }

        if (!isGrounded )
            if (!isGrounded)
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
            else
            {
                transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
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
        

    }

    void Jump()
    {
        lastInputJumping = new Vector3(0f, 0f, horizontal_movement);
        rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
        
        isGrounded = false;
        isJumping = true; 
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

        isGrounded = Physics.BoxCast(capsule_collider.bounds.center, transform.localScale / 2, Vector3.down, out ground_hit, Quaternion.identity, extra_height);
    }

    void SlopeCheck()
    {
        RaycastHit slope_ray;
        if(Physics.Raycast(capsule_collider.bounds.center, Vector3.down, out slope_ray, capsule_collider.height / 2 + .5f))
        {
            slope_norm = slope_ray.normal;
            if (slope_norm != Vector3.up)
            {
                //transform.Translate(new Vector3(0f, -slopeforce, .1f) * speed);
                //if (Mathf.Abs(slope_ray.transform.rotation.x) >= .3f)//too steep

            }
        }
    }

}
