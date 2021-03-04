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

public class LedgeLocator : MonoBehaviour
{
    public AnimationClip clip;
    public float climbingHorizontalOffset;
    public float offsetLedgeClimbing = -0.2f;
    public float securityOffsetLedgeClimbing = 0.51f;
    public float ledgeDistance = 0.5f; 

    private Vector3 topOfPlayer;
    private Vector3 securityRayForClimbing; 
    private GameObject ledge;
    public float animationTime = .5f;
    private bool falling;
    private bool moved;


    [HideInInspector]
    public bool grabbingLedge;
    private CapsuleCollider col;
    private Rigidbody rb;
    private Animator anim;

    public KeyCode climb_up;
    public KeyCode let_go;
    private KeyCode horizontalArrow;

  
    private void Start()
    {
        
        col = GetComponent<CapsuleCollider>();
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


                // col.isTrigger = true;
                if(ledge.GetComponent<Platforms>() != null)
                {
                    GameObject playerParent = ledge.GetComponent<Platforms>().playerParent;
                    Transform platformTransform = ledge.GetComponent<Platforms>().transform;
                    playerParent.transform.parent = platformTransform;
                }
                AdjustPlayerPosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + climbingHorizontalOffset),ledge.transform);
                rb.velocity = Vector3.zero;
                rb.useGravity = false; 
                GetComponent<Movement>().enabled = false;
                
            }
            else
            {
                //col.isTrigger = false; 
               
                rb.useGravity = true;
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

        if (grabbingLedge && ( Input.GetKey(climb_up) ||Input.GetKey(horizontalArrow) || Input.GetKeyDown(KeyCode.Space)) && ledge!= null)
        {
            //anim.SetBool("LedgeHanging", false);
            if(transform.localScale.z > 0)
            {
                StartCoroutine(ClimbingLedge(new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y + .2f, transform.position.z + climbingHorizontalOffset), animationTime, ledge.transform));

            }
            else
            {
                StartCoroutine(ClimbingLedge(new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y + .2f, transform.position.z - climbingHorizontalOffset), animationTime, ledge.transform));

            }
            //StartCoroutine(ClimbingLedge(new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y + .2f,(transform.position.z + climbingHorizontalOffset) *  transform.TransformDirection(Vector3.forward * transform.localScale.z).z ), animationTime));

           
        }
        if (grabbingLedge && Input.GetKey(let_go))
        {
            ledge = null;
            moved = false;
            ////anim.SetBool("LedgeHanging", false);
            falling = true;
            rb.useGravity = true;
            GetComponent<Movement>().enabled = true;
            Invoke("NotFalling", .5f);
        }
    }

    protected virtual IEnumerator ClimbingLedge(Vector3 topOfPlatform, float duration, Transform topOfPlatformTransform)
    {
        Vector3 localPosition = topOfPlatformTransform.InverseTransformPoint(topOfPlatform);

        float time = 0;
        Vector3 startValue = transform.position;
        //transform.position = topOfPlatform;
        if(ledge.GetComponent<Platforms>() != null)
        {
            float platformSpeed = ledge.GetComponent<Platforms>().speed; 
        }
        while (time < duration)
        {
            //anim.SetBool("LedgeClimbing", true);
            transform.position = Vector3.Lerp(startValue, topOfPlatformTransform.TransformPoint(localPosition), time / duration);
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        //yield return null;
        ledge = null;
        moved = false;
        grabbingLedge = false;
        //anim.SetBool("LedgeClimbing", false);
    }

    protected virtual void AdjustPlayerPosition(Vector3 topOfPlatform, Transform topOfPlatformTransform)
    {
        Vector3 localPosition = topOfPlatformTransform.InverseTransformPoint(topOfPlatform);
        if (!moved)
        {
            moved = true;
            if (transform.localScale.z > 0)
            {
                //transform.position = topOfPlatformTransform.TransformPoint(localPosition);
                transform.position = new Vector3(transform.position.x, (ledge.GetComponent<Collider>().bounds.max.y - col.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset, (ledge.GetComponent<Collider>().bounds.min.z - col.bounds.extents.z) + ledge.GetComponent<Ledge>().hangingHorizontalOffset);
            }
            else
            {
                //transform.position = topOfPlatformTransform.TransformPoint(localPosition);
                transform.position = new Vector3(transform.position.x, (ledge.GetComponent<Collider>().bounds.max.y - col.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset, (ledge.GetComponent<Collider>().bounds.max.z + col.bounds.extents.z) - ledge.GetComponent<Ledge>().hangingHorizontalOffset);
            }
        }
    }
    private void OnDrawGizmos()
    {
       
        Gizmos.color = Color.blue; 
        Gizmos.DrawLine(topOfPlayer , topOfPlayer + transform.TransformDirection(new Vector3(0,0,ledgeDistance) * transform.localScale.z) );
        Gizmos.color = Color.red;
        Gizmos.DrawLine(securityRayForClimbing, securityRayForClimbing + transform.TransformDirection(new Vector3(0, 0, ledgeDistance) * transform.localScale.z));
    }
    protected virtual void NotFalling()
    {
        falling = false;
    }
}
