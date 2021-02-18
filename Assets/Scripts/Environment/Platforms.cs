using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    //public Transform[] Waypoints;
    //public float moveSpeed;
    //public int pause;
    //int waypointIndex = 0;

    //float startTime;
    //float journeyLength;

    public float speed;

    public Transform pointB;

    IEnumerator Start()
    {
        var pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB.position, speed));
            yield return new WaitForSeconds(pause);
            yield return StartCoroutine(MoveObject(transform, pointB.position, pointA, speed));
            yield return new WaitForSeconds(pause);
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    //private void Start()
    //{
    //    // Places the platform at the start of the waypoints[]
    //    transform.position = Waypoints[waypointIndex].transform.position;
    //    startTime = Time.time;

    //    //journeyLength = Vector3.Distance(transform.position, Waypoints[waypointIndex].transform.position);
    //}

    //void FixedUpdate()
    //{
    //    Move();
    //}

    //void Move()
    //{
    //    if (Vector3.Distance(transform.position, Waypoints[waypointIndex].transform.position) > .01f)
    //    {
    //        float timeSinceStarted = Time.time - startTime;
    //        float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

    //        // Handles moving the platform from A to B or B to A
    //        transform.position = Vector3.Lerp(transform.position, Waypoints[waypointIndex].transform.position, percentageComplete);
    //    }
    //    else
    //    {
    //        StartCoroutine(Delaying());
    //    }
    //}

    //void ChangeWaypoint()
    //{
    //    // Increases the waypointIndex when the platform reaches the current waypoint ahead
    //    if (transform.position == Waypoints[waypointIndex].transform.position)
    //    {
    //        Debug.Log("here");
    //        waypointIndex += 1;
    //    }

    //    // Resets the waypointIndex once the platform reaches the end of the Waypoints[]
    //    if (waypointIndex == Waypoints.Length)
    //    {
    //        Debug.Log("there");
    //        waypointIndex = 0;
    //    }
    //}

    //IEnumerator Delaying()
    //{
    //    yield return new WaitForSeconds(pause);
    //    ChangeWaypoint();

    //    startTime = Time.time;
    //}

    // Allows the player and other objects to stick to the platform and move on it
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
