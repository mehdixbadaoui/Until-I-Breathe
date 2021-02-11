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
    //public float climbingHorizontalOffset;
    public float offsetLedgeClimbing = -0.2f;
    public float securityOffsetLedgeClimbing = 0.51f; 

    private Vector3 topOfPlayer;
    private Vector3 securityRayForClimbing; 
    private GameObject ledge;
    private float animationTime = .5f;
    private bool falling;
    private bool moved;


    [HideInInspector]
    public bool grabbingLedge;
    private Collider col;
    private Rigidbody rb;
    private Animator anim;


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
            //if (transform.localScale.x > 0)
            //{
            //    topOfPlayer = new Vector3(0, col.bounds.max.y /*+ offsetLedgeClimbing*/, transform.position.z);
            //    middleOfPlayer = new Vector3(0, col.bounds.center.y + offsetLedgeClimbing, transform.position.z);
            //    RaycastHit hit; 
            //    if (Physics.Raycast(topOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, 0.5f) && hit.collider.GetComponent<Ledge>() && Physics.Raycast(middleOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z)))
            //    {
            //        ledge = hit.collider.gameObject;
            //        if (col.bounds.max.y < ledge.GetComponent<Collider>().bounds.max.y && col.bounds.max.y > ledge.GetComponent<Collider>().bounds.center.y)
            //        {
            //            grabbingLedge = true;
            //            anim.SetBool("LedgeHanging", true);
            //        }
            //    }
            //}
            topOfPlayer = new Vector3(0, col.bounds.max.y + offsetLedgeClimbing, transform.position.z);
            securityRayForClimbing = new Vector3(0, col.bounds.max.y + securityOffsetLedgeClimbing, transform.position.z);
            RaycastHit hit;
            RaycastHit hitSecurity; 
            if (!alt_mvt.isGrounded && Physics.Raycast(topOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, 1f) && hit.collider.GetComponent<Ledge>() && !Physics.Raycast(securityRayForClimbing, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hitSecurity, 0.5f))
            {
                if (hit.collider)
                    Debug.Log("exist");
                ledge = hit.collider.gameObject;
                if (col.bounds.max.y < ledge.GetComponent<Collider>().bounds.max.y && col.bounds.max.y > ledge.GetComponent<Collider>().bounds.center.y)
                {
                    grabbingLedge = true;
                    anim.SetBool("LedgeHanging", true);
                }
            }
            //else
            //{
            //    topOfPlayer = new Vector3(0, col.bounds.max.y /*+ offsetLedgeClimbing*/, transform.position.z);
            //    middleOfPlayer = new Vector3(0, col.bounds.center.y + offsetLedgeClimbing, transform.position.z); 

            //    RaycastHit hit;
            //    if (Physics.Raycast(topOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, 0.5f) && hit.collider.GetComponent<Ledge>() && Physics.Raycast(middleOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z)))
            //    {
            //        ledge = hit.collider.gameObject;
            //        if (col.bounds.max.y < ledge.GetComponent<Collider>().bounds.max.y && col.bounds.max.y > ledge.GetComponent<Collider>().bounds.center.y)
            //        {
            //            anim.SetBool("LedgeHanging", true);
            //            grabbingLedge = true;
            //        }
            //    }
            //}
            if (ledge != null && grabbingLedge)
            {
                AdjustPlayerPosition();
                rb.velocity = Vector3.zero;
                rb.isKinematic = true; 
                GetComponent<alt_mvt>().enabled = false;
            }
            else
            {
                rb.isKinematic = false;
                GetComponent<alt_mvt>().enabled = true;
            }
        }
    }

    protected virtual void LedgeHanging()
    {
        if (grabbingLedge && Input.GetAxis("Vertical") > 0)
        {
            anim.SetBool("LedgeHanging", false);
            if (transform.localScale.x > 0)
            {
                StartCoroutine(ClimbingLedge(new Vector3(0, ledge.GetComponent<Collider>().bounds.max.y + col.bounds.extents.y, transform.position.z /*+ climbingHorizontalOffset*/), animationTime - .3f));
            }
            else
            {
                StartCoroutine(ClimbingLedge(new Vector3(0, ledge.GetComponent<Collider>().bounds.max.y + col.bounds.extents.y, transform.position.z /*- climbingHorizontalOffset*/), animationTime - .3f));
            }
        }
        if (grabbingLedge && Input.GetAxis("Vertical") < 0)
        {
            ledge = null;
            moved = false;
            anim.SetBool("LedgeHanging", false);
            falling = true;
            rb.isKinematic = false;
            GetComponent<alt_mvt>().enabled = true;
            Invoke("NotFalling", .5f);
        }
    }

    protected virtual IEnumerator ClimbingLedge(Vector3 topOfPlatform, float duration)
    {
        float time = 0;
        Vector3 startValue = transform.position;
        while (time < duration)
        {
            anim.SetBool("LedgeClimbing", true);
            transform.position = Vector3.Lerp(startValue, topOfPlatform, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        ledge = null;
        moved = false;
        grabbingLedge = false;
        anim.SetBool("LedgeClimbing", false);
    }

    protected virtual void AdjustPlayerPosition()
    {
        if (!moved)
        {
            moved = true;
            if (transform.localScale.z > 0)
            {
                transform.position = new Vector3(0, (ledge.GetComponent<Collider>().bounds.max.y - col.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset, (ledge.GetComponent<Collider>().bounds.min.z - col.bounds.extents.z) + ledge.GetComponent<Ledge>().hangingHorizontalOffset);
            }
            else
            {
                transform.position = new Vector3(0, (ledge.GetComponent<Collider>().bounds.max.y - col.bounds.extents.y - .5f) + ledge.GetComponent<Ledge>().hangingVerticalOffset, (ledge.GetComponent<Collider>().bounds.max.z + col.bounds.extents.z) - ledge.GetComponent<Ledge>().hangingHorizontalOffset);
            }
        }
    }
    private void OnDrawGizmos()
    {
       
        Gizmos.color = Color.blue; 
        Gizmos.DrawLine(topOfPlayer, topOfPlayer + transform.TransformDirection(Vector3.forward * transform.localScale.z));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(securityRayForClimbing, securityRayForClimbing + transform.TransformDirection(Vector3.forward * transform.localScale.z));
    }
    protected virtual void NotFalling()
    {
        falling = false;
    }
}
