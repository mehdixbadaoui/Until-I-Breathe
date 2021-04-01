using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public GameObject playerParent;
    public GameObject st2;
    public GameObject cam;
    public Transform startPoint, endPoint;
    public float secondsItTakes;
    //public float speed;
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
        {
            Move();
        }
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
                    //float distCovered = (Time.time - startTime) * speed;

                    //float fractionOfJourney = distCovered / journeyLength;

                    //transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);

                    //transform.position = Vector3.MoveTowards(departTarget.position, destinationTarget.position, currentSpeed * Time.deltaTime);

                    StartCoroutine(Move_Routine(departTarget.position, destinationTarget.position));
                }
                else
                {
                    isWaiting = true;
                    StartCoroutine(changeDelay());
                }
            }

        }
    }

    private IEnumerator Move_Routine( Vector3 from, Vector3 to)
    {
        float t = 0f;
        while (t < 1f && !isWaiting)
        {
            t += Time.smoothDeltaTime / secondsItTakes;
            transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, t)));
            yield return null;
        }

        transform.position = to;
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

    // Allows the player, ST2 & the camera to stick to the platform and move on it
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "uni" ) && !playerOn )
        {
            other.isTrigger = false; 
            playerOn = true;
            playerParent.transform.parent = transform;
            st2.transform.parent = playerParent.transform;
            cam.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "uni" ) && playerOn && !other.isTrigger)
        {
            playerOn = false;
            st2.transform.parent = null;
            playerParent.transform.parent = null;
            cam.transform.parent = null;
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
}
