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
    private Vector3 lastInput;

    public Vector3 direction;

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
        if(can_jump || lastInput.normalized == new Vector3(0f, 0f, horizontal_movement).normalized)
        {
            transform.Translate(new Vector3(0f, 0f, horizontal_movement) * speed);
        }
        
        if (can_jump)
            if (lastInput.normalized != new Vector3(0f, 0f, horizontal_movement).normalized)
            {
                transform.Translate(new Vector3(0f, 0f, horizontal_movement / 2.5f) * speed);
                //transform.position += horizontal_movement_vector * speed * Time.deltaTime;

            }
               
    }

    void Jump()
    {
        lastInput = new Vector3(0f, 0f, horizontal_movement);  
        rb.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
        
        can_jump = false;
    }

}
