using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public GameObject Uni;
    public GameObject St2;
    public GameObject Cam;
    [HideInInspector] public GameObject playerParent; //for ledgeLocator ref

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

    [HideInInspector]
    public bool isWaiting;
    //private GameObject uni;
    private PlayEventSounds playEvent;

    void Start()
    {

        PlatformLauncherScript = PlatformLauncherGO.GetComponent<PlatformsLauncher>();
        departTarget = startPoint;
        destinationTarget = endPoint;
        //uni = GameObject.FindGameObjectWithTag("uni");
        playEvent = Uni.GetComponent<PlayEventSounds>();
        firstTimeOn = true;
        isWaiting = true;

        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
    }


    void FixedUpdate()
    {
        if (PlatformLauncherScript != null)
        {
            //playEvent.PlayEventWithoutRTPC("Nacelle_depart_event", uni);
            Move();
        }
    }

    private void Move()
    {
        if (PlatformLauncherScript.activate)
        {
            //playEvent.PlayEventWithoutRTPC("Nacelle_loop_event", uni);
            StartCoroutine(PlatformLaunch());
            firstTimeOn = false;

            if (!isWaiting)
            {
                if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
                {
                    StartCoroutine(Move_Routine(transform.position, destinationTarget.position));

                    if (Vector3.Distance(transform.position, destinationTarget.position) < 0.01f) // insure that the platform is at the exact position of its destination
                    {
                        transform.position = destinationTarget.position;
                    }

                }
                else
                {
                    isWaiting = true;
                    StartCoroutine(ChangeDelay());
                }
            }

        }
    }

    private IEnumerator Move_Routine(Vector3 from, Vector3 to)
    {
        float t = 0f;
        while (t < 1f && !isWaiting)
        {
            t += Time.smoothDeltaTime / speed;
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

    IEnumerator ChangeDelay()
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
        if ((other.tag == "uni") && !playerOn)
        {
            other.isTrigger = false;

            playerParent = new GameObject("PlayerParentGO");

            playerOn = true;
            playerParent.transform.parent = transform;
            Uni.transform.parent = playerParent.transform;
            St2.transform.parent = playerParent.transform;
            Cam.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "uni") && playerOn && !other.isTrigger)
        {
            playerOn = false;
            St2.transform.parent = null;
            Uni.transform.parent = null;
            Cam.transform.parent = null;
            Destroy(GameObject.Find("PlayerParentGO"));
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
