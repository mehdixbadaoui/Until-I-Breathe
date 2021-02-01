﻿using System.Collections;
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
    private bool _isGrounded = true;
    
    public KeyCode Jump;
    private float distToGround;
    private Vector3 jumpForce = new Vector3(0,1,0); 
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>(); 
        //_groundChecker = transform.GetChild(0);
        distToGround = capsuleCollider.bounds.extents.y; 
    }
    

void Update()
    {
        //_isGrounded = Physics.CheckSphere(capsuleCollider.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }

        _inputs = Vector3.zero;
        _inputs.z = Input.GetAxis("Horizontal");
        
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;
       
        



        if (Input.GetKey(Jump) && IsGrounded())
        {
            _body.velocity = _body.velocity + jumpForce * JumpHeight; 
            //_body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y) /*/ 30*/, ForceMode.Impulse);
        }



    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}