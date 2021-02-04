using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{

    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;
    //public float fallMultiplier = 2.5f;
    //public float lowJumpMultiplier = 2f;
    private Rigidbody _body;
    private CapsuleCollider capsuleCollider;

    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded;
    private bool _isGrappling = false;
    private bool _isJumping = false;
    

    public KeyCode Jump;
    private float distToGround;

    [Range(1f, 10f)]
    public float speed = 1f;


    private Vector3 jumpForce = new Vector3(0,1,0);

    Vector3 jump = new Vector3(0, 1, 0);
    public float geforce = 2f;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>(); 
        //_groundChecker = transform.GetChild(0);
        distToGround = capsuleCollider.bounds.extents.y;

        _isGrounded = true;
    }


    public bool Grappling
    {
        get { return _isGrappling; }
        set { _isGrappling = value; }
    }



void Update()
    {
/*        //_isGrounded = Physics.CheckSphere(capsuleCollider.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }
*/

        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround);


        if (Input.GetKey(Jump) && (_isGrounded || _isGrappling) && !_isJumping)
        {
            _body.velocity = _body.velocity + jumpForce * JumpHeight;
            _isJumping = true;
            //_body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y) /*/ 30*/, ForceMode.Impulse);
        }







    }


    void FixedUpdate()
    {

        if (_isGrappling)
        {
            speed = 2;

        }
        else
        {
        }

        _inputs = Vector3.zero;
        _inputs.z = Input.GetAxis("Horizontal") * speed;

        if (_inputs != Vector3.zero && !_isGrappling)
        {
            _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
            transform.forward = _inputs;
        }


        // After the fixed update we can jump again if it's gounded again
        _isJumping = false;




        //_body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}
