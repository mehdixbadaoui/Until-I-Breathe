using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsWithButton : MonoBehaviour
{
    public GameObject playerParent;
    public Transform pointA, pointB, pointC, pointD;
    public bool fourPoints;
    public float speed;
    
    Transform currentPoint, goToPoint;

    float startTime;
    bool playerOn;
    bool isWaiting;
    bool launched;
    float journeyLength;

    [HideInInspector]
    public int intDir = 0;
    [HideInInspector]
    public bool readyToGo;

    void Start()
    {
        currentPoint = pointA;
        goToPoint = pointB;

        journeyLength = Vector3.Distance(pointA.position, pointB.position);

        isWaiting = true;
        readyToGo = true;
    }


    void FixedUpdate()
    {
        Launch();
        Move();
        CheckIfReadyToGo();
    }

    private void Move()
    {
        if (!isWaiting)
        {
            launched = true;
            readyToGo = false;

            if (Vector3.Distance(transform.position, goToPoint.position) > 0.01f)
            {
                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(currentPoint.position, goToPoint.position, fractionOfJourney);

                if (Vector3.Distance(transform.position, goToPoint.position) < 0.01f) // insure that the platform is at the exact position of its destination
                {
                    transform.position = goToPoint.position;

                }
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
        if (fourPoints) //if there's 4 points
        {
            if (intDir == 1 && launched)
            {
                currentPoint = pointA;
                goToPoint = pointB;
            }
            else if (intDir == 2 && transform.position == pointB.position)
            {
                currentPoint = pointB;
                goToPoint = pointC;
            }
            else if (intDir == 3 && transform.position == pointC.position)
            {
                currentPoint = pointC;
                goToPoint = pointD;
            }
            else if (intDir == 4 && transform.position == pointD.position)
            {
                currentPoint = pointD;
                goToPoint = pointA;
            }
        }
        else // if there's 3 points
        {
            if (intDir == 1)
            {
                currentPoint = pointA;
                goToPoint = pointB;
            }
            else if (intDir == 2 && transform.position == pointB.position)
            {
                currentPoint = pointB;
                goToPoint = pointC;
            }
            else if (intDir == 3 && transform.position == pointC.position)
            {
                currentPoint = pointC;
                goToPoint = pointA;
            }
        }
    }

    IEnumerator changeDelay()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeDestination();
        startTime = Time.time;

        if (fourPoints)
        {
            if (intDir == 1)
            {
                journeyLength = Vector3.Distance(pointA.position, pointB.position);
            }
            else if (intDir == 2)
            {
                journeyLength = Vector3.Distance(pointB.position, pointC.position);
            }
            else if (intDir == 3)
            {
                journeyLength = Vector3.Distance(pointC.position, pointD.position);
            }
            else if (intDir == 4)
            {
                journeyLength = Vector3.Distance(pointD.position, pointA.position);
            }
        }

        else
        {
            if (intDir == 1)
            {
                journeyLength = Vector3.Distance(pointA.position, pointB.position);
            }
            else if (intDir == 2)
            {
                journeyLength = Vector3.Distance(pointB.position, pointC.position);
            }
            else if (intDir == 3)
            {
                journeyLength = Vector3.Distance(pointC.position, pointA.position);
            }
        }

        isWaiting = false;
    }

    void Launch()
    {
        if (!launched && intDir == 1)
        {
            startTime = Time.time;
            isWaiting = false;
        }
    }

    void CheckIfReadyToGo()
    {
        if (transform.position == goToPoint.position)
            readyToGo = true;
    }

    // Allows the player and other objects to stick to the platform and move on it
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "uni") && !playerOn)
        {
            other.isTrigger = false;
            playerOn = true;
            playerParent.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "uni") && playerOn && !other.isTrigger)
        {
            playerOn = false;
            playerParent.transform.parent = null;
        }
    }
}
