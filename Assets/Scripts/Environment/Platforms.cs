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

    [SerializeField]
    GameObject playerParent;

    [SerializeField]
    Transform startPoint, endPoint;

    [SerializeField]
    float speed;
    [SerializeField]
    float changeDirectionDelay;


    private Transform destinationTarget, departTarget;
    private float startTime;
    private float journeyLength;
    bool isWaiting;
    bool playerOn = false;

    void Start()
    {
        departTarget = startPoint;
        destinationTarget = endPoint;

        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {


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
        if (other.tag == "uni" && !playerOn)
        {
            playerOn = true;
            playerParent.transform.parent = transform;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "uni" && playerOn)
        {
            playerOn = false;
            playerParent.transform.parent = null;
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
