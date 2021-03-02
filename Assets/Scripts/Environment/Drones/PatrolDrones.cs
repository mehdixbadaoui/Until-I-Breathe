using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDrones : MonoBehaviour
{
    // PlayerDetection Script
    PlayerDetection playerDetectionScript;

    public Transform startPoint, endPoint;
    public float speed;
    public float changeDirectionDelay;

    [Range(0f, 1f)]
    public float rotSpeed;

    Transform destinationTarget, departTarget;
    Quaternion desiredRot;
    float startTime;
    float journeyLength;
    bool isWaiting;

    void Start()
    {
        // Ref to the PlayerDetection Script
        playerDetectionScript = GetComponent<PlayerDetection>();

        departTarget = startPoint;
        destinationTarget = endPoint;

        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
    }


    void FixedUpdate()
    {
        if (playerDetectionScript.detected == false)
        {
            Move();
        }
    }

    private void Move()
    {
        if (!isWaiting)
        {
            if (Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
            {
                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, 10 * rotSpeed * Time.deltaTime);
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
            desiredRot = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        }
        else
        {
            departTarget = endPoint;
            destinationTarget = startPoint;
            desiredRot = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
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



    //// Params of moving
    //public Transform pointB;
    //Vector3 pointA;
    //public float timeItTakes;
    //public int pause;



    //private float timeCount = 0.0f;

    //void Start()
    //{
    //    // Ref to the PlayerDetection Script
    //    playerDetectionScript = GetComponent<PlayerDetection>();

    //    //initial position of the drone is the starting point of patrol
    //    pointA = transform.position;

    //    //keeps track of the coroutine instantiated
    //    IEnumerator coDR = ChangeDir();
    //    StartCoroutine(coDR);
    //}

    //void Update()
    //{
    //    timeCount = timeCount + Time.deltaTime;
    //}

    //// Calls the moving function to patrol between point A and B
    //IEnumerator ChangeDir()
    //{
    //    while(true)
    //    {
    //        // From pA -> pB
    //        yield return StartCoroutine(MoveObject(transform, pointA, pointB.position, timeItTakes));
    //        if (playerDetectionScript.canTurn) //if player detected cant turn
    //        {
    //            var desiredRotQA = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
    //            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotQA, timeCount * rotSpeed);
    //            yield return new WaitForSeconds(pause);
    //        }


    //        // From pB -> pA
    //        yield return StartCoroutine(MoveObject(transform, pointB.position, pointA, timeItTakes));
    //        if (playerDetectionScript.canTurn) //if player detected cant turn
    //        {
    //            var desiredRotQB = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
    //            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotQB, timeCount * rotSpeed);
    //            yield return new WaitForSeconds(pause);
    //        }
    //    }
    //}

    //// Actually responsible for moving the drone
    //IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    //{
    //    var i = 0.0f;
    //    var rate = 1.0f / time;
    //    while (i < 1.0f && playerDetectionScript.detected == false)
    //    {
    //        i += Time.deltaTime * rate;
    //        thisTransform.position = Vector3.Lerp(startPos, endPos, i);
    //        yield return null;
    //    }
    //}
}
