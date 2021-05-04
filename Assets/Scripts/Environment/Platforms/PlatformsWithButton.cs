using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsWithButton : MonoBehaviour
{
    public GameObject Uni;
    public GameObject St2;
    public GameObject Cam;
    [HideInInspector] public GameObject playerParent; //for ledgeLocator ref

    public Transform pointA, pointB, pointC, pointD;
    public bool fourPoints;
    public float secondsItTakes;

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

    public GameObject leftdoor;
    private bool doorOpen = true;
    private bool doorismoving = false;

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
                if (doorOpen && !doorismoving)
                {
                    doorismoving = true;
                    doorOpen = false;
                    StartCoroutine(MoveDoor(leftdoor.transform, Vector3.right));
                }

                //float distCovered = (Time.time - startTime) * speed;

                //float fractionOfJourney = distCovered / journeyLength;

                //transform.position = Vector3.Lerp(currentPos, goToPoint.position, fractionOfJourney);

                StartCoroutine(Move_Routine(currentPos, goToPoint.position));

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
        else
        {
            if (!doorOpen && !doorismoving)
            {
                doorismoving = true;
                doorOpen = true;
                StartCoroutine(MoveDoor(leftdoor.transform, Vector3.left));
            }
        }
    }

    private IEnumerator Move_Routine(Vector3 from, Vector3 to)
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

    IEnumerator MoveDoor(Transform door, Vector3 direction)
    {

        Vector3 Gotoposition = new Vector3(door.position.x + direction.x * 1.6f , door.position.y , door.position.z );   
        float elapsedTime = 0;
        float waitTime = 0.5f;

        Vector3 currentPos = door.position;

        while (elapsedTime < waitTime)
        {
            door.position = new Vector3( Mathf.Lerp(currentPos.x, Gotoposition.x, (elapsedTime / waitTime) ) , door.position.y, door.position.z );
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        door.position = new Vector3( Gotoposition.x, door.position.y, door.position.z);
        yield return null;

        doorismoving = false;

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
}