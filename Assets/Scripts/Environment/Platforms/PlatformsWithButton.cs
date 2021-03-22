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
    bool isWaiting;
    bool playerOn;
    bool launched;
    float journeyLength;

    [HideInInspector]
    public int intDir = 0;


    void Start()
    {
        currentPoint = pointA;
        goToPoint = pointB;

        journeyLength = Vector3.Distance(pointA.position, pointB.position);

        isWaiting = true;
    }


    void FixedUpdate()
    {
        Launch();
        Move();
    }

    private void Move()
    {
        if (!isWaiting)
        {
            launched = true; 

            if (Vector3.Distance(transform.position, goToPoint.position) > 0.01f)
            {
                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(currentPoint.position, goToPoint.position, fractionOfJourney);
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

    //// Start is called before the first frame update
    //void Start()
    //{
    //    journeyLengthAB = Vector3.Distance(pointA.position, pointB.position);
    //    journeyLengthBC = Vector3.Distance(pointB.position, pointC.position);
    //    journeyLengthCD = Vector3.Distance(pointC.position, pointD.position);
    //    journeyLengthCA = Vector3.Distance(pointC.position, pointA.position);
    //    journeyLengthDA = Vector3.Distance(pointD.position, pointA.position);

    //    isWaiting = true;
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    Debug.Log(goToPoint);
    //    Move();
    //}

    //void Move()
    //{
    //    ChangeDestination();


    //    if (!isWaiting)
    //    {
    //        if (goToPoint == pointB)
    //        {
    //            float distCovered = (Time.time - startTime) * speed;
    //            float fractionOfJourney = distCovered / journeyLengthAB;

    //            transform.position = Vector3.Lerp(currentPoint.position, goToPoint.position, fractionOfJourney);

    //            if (transform.position == goToPoint.position)
    //            {
    //                isWaiting = true;
    //            }
    //        }

    //        if (goToPoint == pointC)
    //        {
    //            float distCovered = (Time.time - startTime) * speed;
    //            float fractionOfJourney = distCovered / journeyLengthBC;

    //            transform.position = Vector3.Lerp(currentPoint.position, goToPoint.position, fractionOfJourney);

    //            if (transform.position == goToPoint.position)
    //            {
    //                isWaiting = true;
    //            }
    //        }

    //        if (goToPoint == pointD)
    //        {
    //            float distCovered = (Time.time - startTime) * speed;
    //            float fractionOfJourney = distCovered / journeyLengthCD;

    //            transform.position = Vector3.Lerp(currentPoint.position, goToPoint.position, fractionOfJourney);

    //            if (transform.position == goToPoint.position)
    //            {
    //                isWaiting = true;
    //            }
    //        }

    //        if (goToPoint == pointA && fourPoints)
    //        {
    //            float distCovered = (Time.time - startTime) * speed;
    //            float fractionOfJourney = distCovered / journeyLengthDA;

    //            transform.position = Vector3.Lerp(currentPoint.position, goToPoint.position, fractionOfJourney);

    //            if (transform.position == goToPoint.position)
    //            {
    //                isWaiting = true;
    //            }
    //        }

    //        if (goToPoint == pointA && !fourPoints)
    //        {
    //            float distCovered = (Time.time - startTime) * speed;
    //            float fractionOfJourney = distCovered / journeyLengthCA;

    //            transform.position = Vector3.Lerp(currentPoint.position, goToPoint.position, fractionOfJourney);

    //            if (transform.position == goToPoint.position)
    //            {
    //                isWaiting = true;
    //            }
    //        }
    //    }
    //}

    //void ChangeDestination()
    //{
    //    if (fourPoints) //if there's 4 points
    //    {
    //        if (intDir == 1)
    //        {
    //            startTime = Time.time;
    //            currentPoint = pointA;
    //            goToPoint = pointB;
    //        }
    //        else if (intDir == 2 && transform.position == pointB.position)
    //        {
    //            startTime = Time.time;
    //            isWaiting = false;
    //            currentPoint = pointB;
    //            goToPoint = pointC;
    //        }
    //        else if (intDir == 3 && transform.position == pointC.position)
    //        {
    //            startTime = Time.time;
    //            isWaiting = false;
    //            currentPoint = pointC;
    //            goToPoint = pointD;
    //        }
    //        else if (intDir == 4 && transform.position == pointD.position)
    //        {
    //            startTime = Time.time;
    //            isWaiting = false;
    //            currentPoint = pointD;
    //            goToPoint = pointA;
    //        }
    //    }
    //    else // if there's 3 points
    //    {
    //        if (intDir == 1)
    //        {
    //            isWaiting = false;
    //            currentPoint = pointA;
    //            goToPoint = pointB;
    //        }
    //        else if (intDir == 2 && transform.position == pointB.position)
    //        {
    //            currentPoint = pointB;
    //            goToPoint = pointC;
    //        }
    //        else if (intDir == 3 && transform.position == pointC.position)
    //        {
    //            currentPoint = pointC;
    //            goToPoint = pointA;
    //        }
    //    }
    //}


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
