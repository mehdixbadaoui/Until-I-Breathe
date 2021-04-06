﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDrones : MonoBehaviour
{
    public Transform player;

    public float timeToSpotPlayer = .5f;
    public Light spotlight;
    public LayerMask viewMask;

    public Transform pathHolder;
    public float speed = 5;
    public float waitTime = .3f;
    public float turnSpeed = 90;

    float startTime;
    float journeyLength;
    Vector3 velocity = Vector3.zero;

    float playerVisibleTimer;
    bool detected;

    Color originalSpotlightColour;
    GameMaster GM;
    private GameObject uni;
    private PlayEventSounds playEvent;
    private Vector3 distWithUni; 

    void Start()
    {
        GM = FindObjectOfType<GameMaster>();
        originalSpotlightColour = spotlight.color;
        uni = GameObject.FindGameObjectWithTag("uni");
        playEvent = uni.GetComponent<PlayEventSounds>(); 
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        
        StartCoroutine(FollowPath(waypoints));

    }

    void Update()
    {
        distWithUni = playEvent.CalculateDistanceUniFromObject(this.gameObject.transform.position); 
        if (detected)
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            playEvent.PlayEventWithoutRTPC("Drone_fireshot_event", uni); 
            GM.Die();
        }
    }


    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.smoothDeltaTime);

            if (transform.position == targetWaypoint)
            {
                playEvent.PlayEventWithoutRTPC("Drone_stationnaire_event", uni);
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            else
                playEvent.PlayEventWithoutRTPC("Break_Drones_stationnaire_event", uni);
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            Debug.DrawLine(transform.position, player.position);
            if (!Physics.Linecast(transform.position, player.position, viewMask))
            {
                
                detected = true;

            }
            else
            {
                detected = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        detected = false;
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PatrolDrones : MonoBehaviour
//{
//    public Transform player;

//    public float timeToSpotPlayer = .5f;
//    public Light spotlight;
//    public LayerMask viewMask;

//    public Transform pathHolder;
//    public float speed = 5;
//    public float waitTime = .3f;
//    public float turnSpeed = 90;

//    float startTime;
//    float journeyLength;

//    float playerVisibleTimer;
//    bool detected;

//    Color originalSpotlightColour;
//    GameMaster GM;

//    void Start()
//    {
//        GM = FindObjectOfType<GameMaster>();
//        originalSpotlightColour = spotlight.color;

//        Vector3[] waypoints = new Vector3[pathHolder.childCount];
//        for (int i = 0; i < waypoints.Length; i++)
//        {
//            waypoints[i] = pathHolder.GetChild(i).position;
//            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
//        }

//        startTime = Time.time;
//        StartCoroutine(FollowPath(waypoints));

//    }

//    void Update()
//    {
//        if (detected)
//        {
//            playerVisibleTimer += Time.deltaTime;
//        }
//        else
//        {
//            playerVisibleTimer -= Time.deltaTime;
//        }
//        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
//        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

//        if (playerVisibleTimer >= timeToSpotPlayer)
//        {
//            GM.Die();
//        }
//    }


//    IEnumerator FollowPath(Vector3[] waypoints)
//    {
//        transform.position = waypoints[0];

//        int targetWaypointIndex = 1;
//        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
//        transform.LookAt(targetWaypoint);
//        journeyLength = Vector3.Distance(transform.position, targetWaypoint);

//        while(true)
//        {
//            float distCovered = (Time.time - startTime) * (speed/10);

//            float fractionOfJourney = distCovered / journeyLength;

//            transform.position = Vector3.Lerp(transform.position, targetWaypoint, fractionOfJourney);

//            if (Vector3.Distance(transform.position, targetWaypoint) < 0.1f)
//                transform.position = targetWaypoint;

//            if (transform.position == targetWaypoint)
//            {
//                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
//                targetWaypoint = waypoints[targetWaypointIndex];
//                startTime = Time.time;
//                journeyLength = Vector3.Distance(transform.position, targetWaypoint);
//                yield return new WaitForSeconds(waitTime);
//                yield return StartCoroutine(TurnToFace(targetWaypoint));
//            }
//            yield return null;
//        }
//    }

//    IEnumerator TurnToFace(Vector3 lookTarget)
//    {
//        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
//        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

//        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
//        {
//            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
//            transform.eulerAngles = Vector3.up * angle;
//            yield return null;
//        }
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.CompareTag("uni"))
//        {
//            Debug.DrawLine(transform.position, player.position);
//            if (!Physics.Linecast(transform.position, player.position, viewMask))
//            {
//                detected = true;
//            }
//            else
//            {
//                detected = false;
//            }
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        detected = false;
//    }
//}
