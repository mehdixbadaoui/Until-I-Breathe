using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST_Movements : MonoBehaviour
{
    public Transform player;
    public float followSharpness = 0.1f;

    Vector3 _followOffset;

    void Start()
    {
        // Cache the initial offset at time of load/spawn
        _followOffset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Apply that offset to get a target position
        Vector3 targetPosition = player.position + _followOffset;

        // Smooth follow 
        transform.position += (targetPosition - transform.position) * followSharpness;
    }
}
