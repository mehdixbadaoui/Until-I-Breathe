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
    public float animationTime = .5f;
    private Animator anim;

    // For the smooth cam when climbing
    private GameObject camFollow;
    private Vector3 previousCamPos;
    private bool changeCam;
    private GameObject camera;
    public bool camChanged = false;

    // Offsets and distance detection for ledge Climbing
    private float climbingHorizontalOffset = 0.5f;
    private float offsetLedgeClimbing = -0.3f;
    private float securityOffsetLedgeClimbing = 0.01f;
    private float securityOffsetMidClimbing = 0f;
    private float ledgeDistanceDetection = 0.5f; 

    // Vector for ledge detection 
    [HideInInspector] public Vector3 topOfPlayer;
    private Vector3 securityRayForClimbing;
    private Vector3 bottomOfPlayer;
    private Vector3 middleOfPlayer;
    private GameObject ledge;
  
    // Useful boolean 
    public bool falling;
    public bool moved;
    public bool isclimbing = false;
    public bool didClimb = false;
    public bool midLedge = false;

    //Iterator to check after climbing
    public int TimeAfterClimbing;
    public int timeAfterHanging = 0;
    public int offsetTimeHanging = 20;


    [HideInInspector]
    public bool grabbingLedge;
    private CapsuleCollider col;
    private Rigidbody rb;
    

    // Keycode for climbing
    public KeyCode climb_up;
    public KeyCode let_go;
    private KeyCode horizontalArrow;

    public RaycastHit hit;
    public RaycastHit hitSecurity;

    // Uni animator
    public Animator myAnimator;

    //Gizmos
    Vector3 previousPos;
    Vector3 newPos;

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
        
        col = GetComponent<CapsuleCollider>();
        rb =  GetComponent<Rigidbody>();

        //camFollow = GameObject.Find("Camera Follow");

        //Initialize the cam components
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (camera.GetComponent<CameraFollow>().player != null)
        {
            camFollow = camera.GetComponent<CameraFollow>().player.gameObject;
            previousCamPos = camFollow.transform.position;
        }

        // Get the animator 
        myAnimator = GetComponentInChildren<Animator>();

        if (clip != null)
        {
            animationTime = clip.length;
        }

        TimeAfterClimbing = 0;



    }

    private void Update()
    {
        if (camera.GetComponent<CameraFollow>().player != null && camFollow == null)
        {
            camFollow = camera.GetComponent<CameraFollow>().player.gameObject;
        }
        if (camFollow != camera.GetComponent<CameraFollow>().player.gameObject)
        {
            camFollow = camera.GetComponent<CameraFollow>().player.gameObject;
            previousCamPos = camFollow.transform.localPosition;
        }

        // On deplace la camera pendant le climbing 
        if (changeCam && camFollow.transform.parent != null && camFollow.transform.parent.tag == "uni")
        {
            camChanged = true;
            camFollow.transform.position = Vector3.Lerp(camFollow.transform.position
                , new Vector3(camFollow.transform.position.x, GameObject.FindGameObjectWithTag("rig").transform.position.y + previousCamPos.y, GameObject.FindGameObjectWithTag("rig").transform.position.z + previousCamPos.z)
                , 0.1f ) ;
        }
        else if (changeCam)
        {
            //Debug.Log("pas uni");
            Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("LedgeClimb"));
            camChanged = true;
            camFollow.transform.position = Vector3.Lerp(camFollow.transform.position
                , new Vector3(camFollow.transform.position.x, camFollow.transform.position.y, GameObject.FindGameObjectWithTag("rig").transform.position.z)
                , 0.3f);
        }
        else if (camChanged)
        {
            camFollow.transform.localPosition = Vector3.Lerp(camFollow.transform.localPosition , previousCamPos , 0.1f);
            if (Vector3.Distance(camFollow.transform.localPosition , previousCamPos )<0.2f )
            {
                camFollow.transform.localPosition = previousCamPos;
                camChanged = false;
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        CheckForLedge();
        LedgeHanging();
        if (Movement.isGrounded && !isclimbing && !grabbingLedge)
        {
            didClimb = false;
            ledge = null;
            StopAllCoroutines();

        }
        if (didClimb && isclimbing == false)
        {
            TimeAfterClimbing += 1;
            myAnimator.SetInteger("timeafterclimb", TimeAfterClimbing);
        }
        if (grabbingLedge)
        {
            timeAfterHanging += 1;
        }

    }

    protected virtual void CheckForLedge()
    {
        if (!falling)
        {
            // Initialisation of topOfPlayer Raycast and Security Raycast
            topOfPlayer = new Vector3(transform.position.x, col.bounds.max.y + offsetLedgeClimbing, transform.position.z);
            securityRayForClimbing = new Vector3(transform.position.x, col.bounds.max.y + securityOffsetLedgeClimbing, transform.position.z);
            middleOfPlayer = new Vector3(transform.position.x, col.bounds.center.y, transform.position.z);
            bottomOfPlayer = new Vector3(transform.position.x, col.bounds.min.y + securityOffsetMidClimbing, transform.position.z);

            //To climb a ledge the topOfPlayer raycast need to hit with the collider and the Security Raycast don't , the collider also needs to have Ledge script
            if ((!Movement.isGrounded && !Movement.isGrapplin && !didClimb && ledge == null && !grabbingLedge) 
                && Physics.Raycast(topOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, ledgeDistanceDetection) && hit.collider.GetComponent<Ledge>()
                && (!hit.collider.GetComponent<Rigidbody>() || hit.collider.GetComponent<Rigidbody>().isKinematic)
                && !Physics.Raycast(securityRayForClimbing, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hitSecurity, ledgeDistanceDetection * 2))
            {
                if (!hit.collider.isTrigger)
                {
                    ledge = hit.collider.gameObject;
                    if (col.bounds.max.y + offsetLedgeClimbing < ledge.GetComponent<Collider>().bounds.max.y && col.bounds.max.y + offsetLedgeClimbing > ledge.GetComponent<Collider>().bounds.center.y)
                    {
                        grabbingLedge = true;
                        myAnimator.SetBool("LedgeHanging", true);
                        myAnimator.Play("Hangingidle", 0);
                        timeAfterHanging = 0;
                    }
                }
            }

            //To climb a ledge the topOfPlayer raycast need to hit with the collider and the Security Raycast don't , the collider also needs to have Ledge script
            if ((!Movement.isGrounded && !Movement.isGrapplin && !didClimb && ledge == null && !grabbingLedge)
                && Physics.Raycast(bottomOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hit, ledgeDistanceDetection * 0.7f) && hit.collider.GetComponent<Ledge>()
                && (!hit.collider.GetComponent<Rigidbody>() || hit.collider.GetComponent<Rigidbody>().isKinematic)
                && !Physics.Raycast(middleOfPlayer, transform.TransformDirection(Vector3.forward * transform.localScale.z), out hitSecurity, ledgeDistanceDetection * 2))
            {
                if (!hit.collider.isTrigger)
                {
                    isclimbing = true;
                    ledge = hit.collider.gameObject;
                    midLedge = true;
                    StartCoroutine(MidLedge());
                }
            }

            if (ledge != null && midLedge)
            {
                //Check if the ledge is a moving platform or not 
                if (ledge.GetComponent<Platforms>() != null)
                {
                    GameObject playerParent = ledge.GetComponent<Platforms>().playerParent;
                    Transform platformTransform = ledge.GetComponent<Platforms>().transform;
                    playerParent.transform.parent = platformTransform;
                }
                AdjustPlayerMidLedge(new Vector3(transform.position.x, transform.position.y, transform.position.z + climbingHorizontalOffset),ledge.transform);
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                Movement.canMove = false;
            }
            else if (ledge != null && grabbingLedge)
            {
                //Check if the ledge is a moving platform or not 
                if(ledge.GetComponent<Platforms>() != null)
                {
                    GameObject playerParent = ledge.GetComponent<Platforms>().playerParent;
                    Transform platformTransform = ledge.GetComponent<Platforms>().transform;
                    playerParent.transform.parent = platformTransform;
                }
                AdjustPlayerPosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + climbingHorizontalOffset),ledge.transform);
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                Movement.canMove = false;
                //GetComponent<Movement>().enabled = false;
            }
            else
            {               
                rb.useGravity = true;
                //Movement.canMove = true;
                //GetComponent<Movement>().enabled = true;
            }
        }
    }

    protected virtual void LedgeHanging()
    {

        // if uni attached to a ledge and whant to climb
        if (grabbingLedge && 
            (Convert.ToBoolean(inputs.Uni.Climb_Up.ReadValue<float>()) || inputs.Uni.Walk.ReadValue<float>() * transform.localScale.z > 0 || Convert.ToBoolean(inputs.Uni.Jump.ReadValue<float>()) )  
            && ledge!= null && !isclimbing && timeAfterHanging > offsetTimeHanging)
        {
            didClimb = true;
            TimeAfterClimbing = 0;
            isclimbing = true;

            // Start the animation of hanging
            myAnimator.SetBool("LedgeHanging", false);
            
            StartCoroutine(ClimbingLedge());


        }
        // if uni attached but want to let the ledge out
        if (grabbingLedge && (Convert.ToBoolean(inputs.Uni.Let_Go.ReadValue<float>() ) || inputs.Uni.Walk.ReadValue<float>() * transform.localScale.z < 0)
            && ledge != null && !isclimbing && timeAfterHanging > offsetTimeHanging)
        {

            didClimb = true;
            grabbingLedge = false;
            TimeAfterClimbing = 0;
            ledge = null;
            moved = false;
            // Stop the animation of hanging
            myAnimator.SetBool("LedgeHanging", false);
            falling = true;
            rb.useGravity = true;
            Movement.canMove = true;
            //GetComponent<Movement>().enabled = true;
            Invoke("NotFalling", .5f);

        }
    }

    protected virtual IEnumerator MidLedge()
    {
/*        if (ledge.GetComponentInParent<Platforms>())
            previousPos = transform.InverseTransformPoint(GameObject.FindGameObjectWithTag("rig").transform.position);
        else
            previousPos = GameObject.FindGameObjectWithTag("rig").transform.position;*/

        myAnimator.SetBool("MidClimbing", true);

        // In Update, camfollow will follow the animation
        //previousCamPos = camFollow.transform.localPosition;

        //Wait for the beginning of LedgeClimb
        float transitionlength = myAnimator.GetAnimatorTransitionInfo(0).duration;
        yield return new WaitWhile(() => myAnimator.GetCurrentAnimatorStateInfo(0).IsName("MidLedge"));

        //Wait for the end of LedgeClimb
        yield return new WaitWhile(() => myAnimator.GetCurrentAnimatorStateInfo(0).length / 2 > myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        // Stop the animation of climbing
        myAnimator.SetBool("MidClimbing", false);

        Movement.canMove = true;
        rb.useGravity = true;

        ledge = null;
        moved = false;
        midLedge = false;
        isclimbing = false;

    }

    protected virtual IEnumerator ClimbingLedge()
    {
        if (ledge.GetComponentInParent<Platforms>())
            previousPos = transform.InverseTransformPoint(GameObject.FindGameObjectWithTag("rig").transform.position);
        else
            previousPos = GameObject.FindGameObjectWithTag("rig").transform.position;

        myAnimator.SetBool("LedgeClimbing", true);

        // In Update, camfollow will follow the animation
        previousCamPos = camFollow.transform.localPosition;
        changeCam = true;

        //Wait for the beginning of LedgeClimb
        float transitionlength = myAnimator.GetAnimatorTransitionInfo(0).duration;
        yield return new WaitWhile(() => myAnimator.GetCurrentAnimatorStateInfo(0).IsName("LedgeClimb"));
        //yield return new WaitWhile(() => myAnimator.GetCurrentAnimatorClipInfo(0)[1].clip;
        //yield return new WaitWhile(() => myAnimator.GetCurrentAnimatorStateInfo(2).IsName("LedgeClimb"));

        //Wait for the end of LedgeClimb
        yield return new WaitWhile(() => myAnimator.GetCurrentAnimatorStateInfo(0).length / 2 > myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //yield return new WaitForSeconds( (myAnimator.GetCurrentAnimatorStateInfo(0).length / 2) - transitionlength );
        //yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorClipInfo(0)[1].clip.length / 2 );

        if (ledge.GetComponentInParent<Platforms>())
            newPos = transform.InverseTransformPoint(GameObject.FindGameObjectWithTag("rig").transform.position);
        else
            newPos = GameObject.FindGameObjectWithTag("rig").transform.position;

        myAnimator.Play("idle&run", 0);
        //myAnimator.Play("idle&run", 2);

        Vector3 previousCamGlobalPos = camFollow.transform.position;
        transform.position +=  new Vector3(0,newPos.y - previousPos.y + 0.5f, newPos.z - previousPos.z); //topOfPlatformTransform.TransformPoint(localPosition);
        camFollow.transform.position = previousCamGlobalPos;

        // Stop the animation of climbing
        myAnimator.SetBool("LedgeClimbing", false);

        Movement.canMove = true;
        rb.useGravity = true;

        // Camfollow recupere sa position de depart
        changeCam = false;



        /*        float time = 0;
                Vector3 startValue = transform.position;  
                while (time < duration)
                {

                    // Start the animation of climbing
                    myAnimator.SetBool("LedgeClimbing", true);
                    transform.position = Vector3.Lerp(startValue, topOfPlatformTransform.TransformPoint(localPosition), time / duration);
                    time += Time.deltaTime;
                    yield return new WaitForFixedUpdate();
                }*/

        ledge = null;
        moved = false;
        grabbingLedge = false;
        isclimbing = false;

    }

    protected virtual void AdjustPlayerMidLedge(Vector3 topOfPlatform, Transform topOfPlatformTransform)
    {
        Vector3 localPosition = topOfPlatformTransform.InverseTransformPoint(topOfPlatform);
        // Si on est vers la droite
        if (transform.localScale.z > 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y, ledge.GetComponent<Collider>().bounds.min.z + 0.5f) , 0.3f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, ledge.GetComponent<Collider>().bounds.max.y, ledge.GetComponent<Collider>().bounds.max.z - 0.5f), 0.3f);
        }

        /*        if (!moved)
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
        */
    }


    protected virtual void AdjustPlayerPosition(Vector3 topOfPlatform, Transform topOfPlatformTransform)
    {
        Vector3 localPosition = topOfPlatformTransform.InverseTransformPoint(topOfPlatform);
        // Si on est vers la droite
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

    //Representing the topOfPlayer security Raycasts
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue; 
        Gizmos.DrawLine(topOfPlayer , topOfPlayer + transform.TransformDirection(new Vector3(0,0,ledgeDistanceDetection) * transform.localScale.z) );
        Gizmos.color = Color.red;
        Gizmos.DrawLine(securityRayForClimbing, securityRayForClimbing + transform.TransformDirection(new Vector3(0, 0, ledgeDistanceDetection * 2) * transform.localScale.z));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomOfPlayer, bottomOfPlayer + transform.TransformDirection(new Vector3(0, 0, ledgeDistanceDetection) * transform.localScale.z));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(middleOfPlayer, middleOfPlayer + transform.TransformDirection(new Vector3(0, 0, ledgeDistanceDetection * 0.7f) * transform.localScale.z ));

        /*
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(newPos, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(previousPos, 0.2f);
        */

    }
    protected virtual void NotFalling()
    {
        falling = false;
    }

}
