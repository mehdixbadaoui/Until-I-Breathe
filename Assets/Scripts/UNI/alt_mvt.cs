using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alt_mvt : MonoBehaviour
{
    public float speed = .2f;
    float horizontal_movement;

    public float jump_force = .5f;
    public static bool can_jump = false;

    float vertical_movement;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_movement = Input.GetAxis("Horizontal");
        vertical_movement = Input.GetAxisRaw("Vertical");
        //horizontal_movement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.Space) && can_jump)
            Jump();

        if (Input.GetKey(KeyCode.S))
            GetComponent<CapsuleCollider>().height = 1;
        else
            GetComponent<CapsuleCollider>().height = 1.5f;

    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
        can_jump = false;
    }

    void Crouch()
    {
        GetComponent<CapsuleCollider>().height = 1;
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider.tag == "ground")
    //        can_jump = true;
    //}


}
