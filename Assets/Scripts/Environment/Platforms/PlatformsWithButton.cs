using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsWithButton : MonoBehaviour
{
    public GameObject playerParent;
    public Transform pointA, pointB, pointC, pointD;
    public bool fourPoints;
    public float speed;
    
    Transform goToPoint;
    Vector3 currentPos;


    float startTime;
    bool playerOn;
    bool isWaiting;
    bool launched;
    float journeyLength;

    [HideInInspector]
    public int intDir = 0;
    [HideInInspector]
    public bool readyToGo;

    private void Awake()
    {
        currentPos = transform.position;
    }

    void Start()
    {
        //currentPoint = pointA;
        goToPoint = pointB;

        journeyLength = Vector3.Distance(currentPos, pointB.position);

        isWaiting = true;
        readyToGo = true;
    }


    void FixedUpdate()
    {
        FetchCurrentPosition();
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

                transform.position = Vector3.Lerp(currentPos, goToPoint.position, fractionOfJourney);

                if (Vector3.Distance(transform.position, goToPoint.position) < 0.01f) // insure that the platform is at the exact position of its destination
                {
                    transform.position = goToPoint.position;
                }
            }
            else
            {
                isWaiting = true;
                StartCoroutine(ChangeDelay());
            }
        }
    }

    void ChangeDestination()
    {
        if (fourPoints) //if there's 4 points
        {
            if (intDir == 1 && launched)
            {
                goToPoint = pointB;
            }
            else if (intDir == 2)
            {
                goToPoint = pointC;
            }
            else if (intDir == 3)
            {
                goToPoint = pointD;
            }
            else if (intDir == 4)
            {
                goToPoint = pointA;
            }
        }
        else // if there's 3 points
        {
            if (intDir == 1)
            {
                goToPoint = pointB;
            }
            else if (intDir == 2)
            {
                goToPoint = pointC;
            }
            else if (intDir == 3)
            {
                goToPoint = pointA;
            }
        }
    }

    IEnumerator ChangeDelay()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeDestination();
        startTime = Time.time;

        if (fourPoints)
        {
            if (intDir == 1)
            {
                journeyLength = Vector3.Distance(currentPos, pointB.position);
            }
            else if (intDir == 2)
            {
                journeyLength = Vector3.Distance(currentPos, pointC.position);
            }
            else if (intDir == 3)
            {
                journeyLength = Vector3.Distance(currentPos, pointD.position);
            }
            else if (intDir == 4)
            {
                journeyLength = Vector3.Distance(currentPos, pointA.position);
            }
        }

        else
        {
            if (intDir == 1)
            {
                journeyLength = Vector3.Distance(currentPos, pointB.position);
            }
            else if (intDir == 2)
            {
                journeyLength = Vector3.Distance(currentPos, pointC.position);
            }
            else if (intDir == 3)
            {
                journeyLength = Vector3.Distance(currentPos, pointA.position);
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
        {
            readyToGo = true;
        }
    }

    void FetchCurrentPosition()
    {
        if (readyToGo)
            currentPos = transform.position;
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
