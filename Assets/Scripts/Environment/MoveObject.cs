using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public float speed;
    public float changeDirectionDelay;

    Transform destinationTarget, departTarget;
    float startTime;
    float journeyLength;

    private bool isWaiting;

    void Start()
    {
        departTarget = startPoint;
        destinationTarget = endPoint;

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
}
