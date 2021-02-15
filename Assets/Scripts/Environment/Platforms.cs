using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public Transform[] Waypoints;
    public float moveSpeed = 5f;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Places the platform at the start of the waypoints[]
        transform.position = Waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        // Handles moving the platform from A to B or B to A
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        // Increases the waypointIndex when the platform reaches the current waypoint ahead
        if(transform.position == Waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        // Resets the waypointIndex once the platform reaches the end of the Waypoints[]
        if(waypointIndex == Waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
