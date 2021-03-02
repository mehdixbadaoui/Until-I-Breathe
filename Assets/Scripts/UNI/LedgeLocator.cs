/*This script is part of a larger solution, but can be used as a standalone
 * script; if you want to use this as a standalone script, make sure everything
 * that is commented off in psuedocode is no longer commented out, and that
 * you delete the Initialization method found within this script, as well as
 * change every reference of character.grabbingLedge to just grabbingLedge.
 
   If you do want to use this with my larger solution, then you don't need to 
   make any changes to this script
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LedgeLocator : MonoBehaviour
{
    private Inputs inputs;

    public AnimationClip clip;
    public float climbingHorizontalOffset;
    public float offsetLedgeClimbing = -0.2f;
    public float securityOffsetLedgeClimbing = 0.51f;
    public float ledgeDistance = 0.2f; 

    private Vector3 topOfPlayer;
    private Vector3 securityRayForClimbing; 
    private GameObject ledge;
    public float animationTime = .5f;
    private bool falling;
    private bool moved;


    [HideInInspector]
    public bool grabbingLedge;
    private Collider col;
    private Rigidbody rb;
    private Animator anim;

    public KeyCode climb_up;
    public KeyCode let_go;
    private KeyCode horizontalArrow;

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

    private void Start()
    {
        col = GetComponent<Collider>();
        rb =  GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if (clip != null)
        {
            animationTime = clip.length;
        }

        
    }


    //protected override void Initializtion()
    //{
    //    base.Initializtion();
    //    //if(clip != null)
    //    //{
    //    //    animationTime = clip.length;
    //    //}
    //}

    protected virtual void FixedUpdate()
    {
        CheckForLedge();
        LedgeHanging();
    }

    protected virtual void CheckForLedge()
    {
        if (!falling)
        {
           
            topOfPlayer = new Vector3(transform.position.x, col.bounds.max.y + offsetLedgeClimbing, transform.position.z);
            securityRayForClimbing = new Vector3(0, col.bounds.max.y + securityOffsetLedgeClimbing, transform.position.z);
            RaycastHit hit;
            RaycastHit hitSecurity;
            if ((!Movement.isGrounded && !Movement.isGrapplin ) && Physics.Raycast(topOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, ledgeDistance) && hit.collider.GetComponent<Ledge>() && !Physics.Raycast(securityRayForClimbing, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hitSecurity, ledgeDistance) /*&& !hit.collider.isTrigger*/)
            {
                if (!hit.collider.isTrigger)
                {
                    ledge = hit.collider.gameObject;
                    if (col.bounds.max.y + offsetLedgeClimbing < ledge.GetComponent<Collider>().bounds.max.y && col.bounds.max.y + offsetLedgeClimbing > ledge.GetComponent<Collider>().bounds.center.y)
                    {
                        grabbingLedge = true;
                        //anim.SetBool("LedgeHanging", true);
                    }
                }
                    //Debug.Log("exist");
                
            }
            
            if (ledge != null && grabbingLedge)
            {
                AdjustPlayerPosition();
                rb.velocity = Vector3.zero;
                rb.isKinematic = true; 
                GetComponent<Movement>().enabled = false;
            }
            else
            {
                rb.isKinematic = false;
                GetComponent<Movement>().enabled = true;
            }
        }
    }

    protected virtual void LedgeHanging()
    {
        if (transform.localScale.z > 0)
        {
            horizontalArrow = KeyCode.D;
        }
        else
        {
            horizontalArrow = KeyCode.Q;

        }

        if (grabbingLedge && (Convert.ToBoolean(inputs.Uni.Climb_Up.ReadValue<float>()) || inputs.Uni.Walk.ReadValue<float>() != 0 || Convert.ToBoolean(inputs.Uni.Jump.ReadValue<float>())) )
        {
            //anim.SetBool("LedgeHanging", false);
            if(transform.localScale.z > 0)
            {
                StartCoroutine(ClimbingLedge(new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y + .2f, transform.position.z + climbingHorizontalOffset), animationTime));

            }
            else
            {
                StartCoroutine(ClimbingLedge(new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y + .2f, transform.position.z - climbingHorizontalOffset), animationTime));

            }
            //StartCoroutine(ClimbingLedge(new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y + .2f,(transform.position.z + climbingHorizontalOffset) *  transform.TransformDirection(Vector3.forward * transform.localScale.z).z ), animationTime));

           
        }
        if (grabbingLedge && Convert.ToBoolean(inputs.Uni.Let_Go.ReadValue<float>()))
        {
            ledge = null;
            moved = false;
            //anim.SetBool("LedgeHanging", false);
            falling = true;
            rb.isKinematic = false;
            GetComponent<Movement>().enabled = true;
            Invoke("NotFalling", .5f);
        }
    }

    protected virtual IEnumerator ClimbingLedge(Vector3 topOfPlatform, float duration)
    {
        float time = 0;
        Vector3 startValue = transform.position;
        //transform.position = topOfPlatform;
        while (time < duration)
        {
            //anim.SetBool("LedgeClimbing", true);
            transform.position = Vector3.Lerp(startValue, topOfPlatform, time / duration);
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        //yield return null;
        ledge = null;
        moved = false;
        grabbingLedge = false;
        //anim.SetBool("LedgeClimbing", false);
    }

    protected virtual void AdjustPlayerPosition()
    {
        if (!moved)
        {
            moved = true;
            if (transform.localScale.z > 0)
            {
                transform.position = new Vector3(transform.position.x, (ledge.GetComponent<Collider>().bounds.max.y - col.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset, (ledge.GetComponent<Collider>().bounds.min.z - col.bounds.extents.z) + ledge.GetComponent<Ledge>().hangingHorizontalOffset);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, (ledge.GetComponent<Collider>().bounds.max.y - col.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset, (ledge.GetComponent<Collider>().bounds.max.z + col.bounds.extents.z) - ledge.GetComponent<Ledge>().hangingHorizontalOffset);
            }
        }
    }
    private void OnDrawGizmos()
    {
       
        Gizmos.color = Color.blue; 
        Gizmos.DrawLine(topOfPlayer , topOfPlayer + transform.TransformDirection(Vector3.forward * transform.localScale.z) );
        Gizmos.color = Color.red;
        Gizmos.DrawLine(securityRayForClimbing, securityRayForClimbing + transform.TransformDirection(Vector3.forward * transform.localScale.z));
    }
    protected virtual void NotFalling()
    {
        falling = false;
    }
}
