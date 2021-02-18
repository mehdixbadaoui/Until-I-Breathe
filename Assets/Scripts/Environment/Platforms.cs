using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public Transform[] Waypoints;
    public float moveSpeed = 5f;
    int waypointIndex = 0;

    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        // Handles moving the platform from A to B or B to A
        transform.position = Vector3.Lerp(transform.position, Waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        // Increases the waypointIndex when the platform reaches the current waypoint ahead
        if (Vector3.Distance(transform.position, Waypoints[waypointIndex].transform.position) < 0.01f)
        {
            waypointIndex += 1;
        }

        // Resets the waypointIndex once the platform reaches the end of the Waypoints[]
        if (waypointIndex == Waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

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
