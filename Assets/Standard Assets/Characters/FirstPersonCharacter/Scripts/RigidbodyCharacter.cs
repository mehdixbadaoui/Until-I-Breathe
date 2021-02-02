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

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private CapsuleCollider capsuleCollider;
    public KeyCode Jump;
    private float distToGround; 
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>(); 
        //_groundChecker = transform.GetChild(0);
        distToGround = capsuleCollider.bounds.extents.y; 
    }
    bool IsGrounded() {
    return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

void Update()
    {
        //_isGrounded = Physics.CheckSphere(capsuleCollider.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


        _inputs = Vector3.zero;
        _inputs.z = Input.GetAxis("Horizontal");
        
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetKey(Jump) && IsGrounded())
        {
              
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y)/30, ForceMode.VelocityChange);
        }
        
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}