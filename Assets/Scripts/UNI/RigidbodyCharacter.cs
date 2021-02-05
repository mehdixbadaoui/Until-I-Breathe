using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{

    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float checkRadius;  
    public LayerMask Ground;
    //public float fallMultiplier = 2.5f;
    //public float lowJumpMultiplier = 2f;
    private Rigidbody _body;
    private CapsuleCollider capsuleCollider;

    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded ;
    private bool _isGrappling = false;

    public KeyCode Jump;
    private float distToGround;

    [Range(1f, 10f)]
    public float speed = 1f;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping; 

    
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
        _isGrounded = Physics.CheckSphere(transform.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        //_isGrounded = Physics.OverlapSphere(transform.position, checkRadius, Ground, QueryTriggerInteraction.Ignore);

        if (_isGrappling)
        {
            speed = 2;
        }
        else
        {
            speed = 1;
        }

        _inputs = Vector3.zero;
        _inputs.z = Input.GetAxis("Horizontal") * speed;


        if (_inputs != Vector3.zero )
            transform.forward = _inputs;
       
        



        if (Input.GetKeyDown(Jump) && _isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; 
            _body.velocity = _body.velocity + Vector3.up * JumpHeight; 
            
        }
        if (Input.GetKey(Jump) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                _body.velocity = _body.velocity + Vector3.up * JumpHeight;
                jumpTimeCounter -= Time.deltaTime; 
            }
            else
            {
                isJumping = false; 
            }
        }
        if (Input.GetKeyUp(Jump))
        {
            isJumping = false; 
        }


    }


    void FixedUpdate()
    {
        if(IsGrounded() )
            _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
        else
            _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime / 2.0f );
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        //return Physics.OverlapSphere(transform.position, checkRadius, Ground); 
    }
}
