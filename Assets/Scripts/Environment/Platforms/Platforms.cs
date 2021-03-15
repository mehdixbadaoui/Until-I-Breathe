using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    //public Transform pointB;
    //Vector3 pointA;
    //public float timeItTakes;
    //public int pause;

    public GameObject playerParent;
    public Transform startPoint, endPoint;
    public float speed;
    public float changeDirectionDelay;

    public GameObject PlatformLauncherGO;
    public float delayToLaunch;

    PlatformsLauncher PlatformLauncherScript;
    Transform destinationTarget, departTarget;
    float startTime;
    float journeyLength;
    bool playerOn = false;
    bool firstTimeOn;
    private CapsuleCollider col; 

    [HideInInspector]
    public bool isWaiting;

    void Start()
    {

        PlatformLauncherScript = PlatformLauncherGO.GetComponent<PlatformsLauncher>();
        col = GetComponent<CapsuleCollider>();
        departTarget = startPoint;
        destinationTarget = endPoint;

        firstTimeOn = true;
        isWaiting = true;

        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
    }


    void FixedUpdate()
    {
        if (PlatformLauncherScript != null)
            Move();
    }

    private void Move()
    {
        if (PlatformLauncherScript.activate)
        {
            StartCoroutine(PlatformLaunch());
            firstTimeOn = false;

            if (!isWaiting)
            {
                if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
                {
                    float distCovered = (Time.time - startTime) * speed;

                    float fractionOfJourney = distCovered / journeyLength;

                    transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
                }
                else
                {
                    isWaiting = true;
                    StartCoroutine(changeDelay());
                }
            }

        }
    }

    void ChangeDestination()
    {
        if (departTarget == endPoint && destinationTarget == startPoint)
        {
            departTarget = startPoint;
            destinationTarget = endPoint;
        }
        else
        {
            departTarget = endPoint;
            destinationTarget = startPoint;
        }
    }
    
    IEnumerator changeDelay()
    {
        yield return new WaitForSeconds(changeDirectionDelay);
        ChangeDestination();
        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
        isWaiting = false;
    }

    // Allows the player and other objects to stick to the platform and move on it
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "uni" ) && !playerOn )
        {
            other.isTrigger = false; 
            playerOn = true;
            playerParent.transform.parent = transform;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "uni" ) && playerOn && !other.isTrigger)
        {
            
            playerOn = false;
            playerParent.transform.parent = null;
        }
    }

    IEnumerator PlatformLaunch()
    {
        if (firstTimeOn && PlatformLauncherScript.activate)
        {
            yield return new WaitForSeconds(delayToLaunch);
            isWaiting = false;
            startTime = Time.time;
        }
    }

    //void Start()
    //{
    //    //initial position of the platform is the start
    //    pointA = transform.position;
    //    StartCoroutine(ChangeDir());
    //}

    //IEnumerator ChangeDir()
    //{
    //    //infinite loop
    //    while (true)
    //    {
    //        yield return StartCoroutine(MoveObject(transform, pointA, pointB.position, timeItTakes));
    //        yield return new WaitForSeconds(pause);
    //        yield return StartCoroutine(MoveObject(transform, pointB.position, pointA, timeItTakes));
    //        yield return new WaitForSeconds(pause);
    //    }
    //}

    //IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    //{
    //    var i = 0.0f;
    //    var rate = 1.0f / time;
    //    while (i < 1.0f)
    //    {
    //        i += Time.deltaTime * rate;
    //        //thisTransform.position = Vector3.Lerp(startPos, endPos, i);
    //        thisTransform.position = Vector3.MoveTowards(startPos, endPos, timeItTakes * Time.deltaTime);
    //        yield return null;
    //    }
    //}
}
