using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alt_mvt : MonoBehaviour
{
    public float speed = .2f;
    float horizontal_movement;

    public float jump_force = .5f;
    public static bool isGrounded = false;

    float vertical_movement;
    private Vector3 lastInput;

    public Vector3 direction;

    Rigidbody rb;
    [HideInInspector]
    public bool isFacingLeft;
    private Vector3 facingLeft;

    RaycastHit ground_hit;
    CapsuleCollider capsule_collider;
    public float extra_height = .3f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        facingLeft = new Vector3(1, transform.localScale.y, -transform.localScale.z);
        capsule_collider = GetComponent<CapsuleCollider>();


        Debug.Log(capsule_collider.height);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_movement = Input.GetAxis("Horizontal");
        vertical_movement = Input.GetAxisRaw("Vertical");


        //horizontal_movement = Input.GetAxisRaw("Horizontal");
 

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        if (Input.GetKey(KeyCode.S))
            capsule_collider.height = 1;
        else
            capsule_collider.height = 1.5f;

        //Debug.Log(isGrounded);

    }

    private void FixedUpdate()
    {
        check_ground();

        if (isGrounded || lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized)
        {
            transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
        }
        
        if (!isGrounded)
            if (lastInput.normalized != new Vector3(0f, 0f, horizontal_movement).normalized)
            {
                transform.Translate(new Vector3(0f, 0f, horizontal_movement / 2.5f) * speed);
                //transform.position += horizontal_movement_vector * speed * Time.deltaTime;

            }
        if (horizontal_movement != 0)
        {

            if (horizontal_movement > 0 && isFacingLeft)
            {
                isFacingLeft = false;
                //Flip();
            }
            if (horizontal_movement < 0 && !isFacingLeft)
            {
                isFacingLeft = true;
                //Flip();
            }
        }

    }

    void Jump()
    {
        lastInput = new Vector3(0f, 0f, horizontal_movement);  
        rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
        
        //isGrounded = false;
    }

    void Crouch()
    {
        capsule_collider.height = 1;
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
            transform.localScale = new Vector3(1, transform.localScale.y, - transform.localScale.z);
        }
    }

    void check_ground()
    {

        isGrounded = Physics.BoxCast(capsule_collider.bounds.center, transform.lossyScale / 2, Vector3.down, out ground_hit,  Quaternion.identity, extra_height);
        if (isGrounded)
            Debug.Log(ground_hit.collider.name);
        else
            Debug.Log("nothing");
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(capsule_collider.bounds.center, Vector3.down * (extra_height));
        //Gizmos.DrawSphere(capsule_collider.bounds.center, extra_height);
        
    }


}
