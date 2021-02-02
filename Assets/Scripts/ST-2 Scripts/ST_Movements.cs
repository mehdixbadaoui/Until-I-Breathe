using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST_Movements : MonoBehaviour
{
    public Transform player;
    public float followSharpness = 0.1f;
    public float rotationSpeed = 5f; 
    public Vector3 startOffset;

    Vector3 followOffset;

    bool isFollowing;
    bool hasFoundGip;

    void Start()
    {
        // Initiliaze the position of ST-2 at the start of  the game
        transform.position = player.position + startOffset;
        isFollowing = true;


        // Cache the initial offset at time of load/spawn
        followOffset = transform.position - player.position;
    }

    private void Update()
    {
        
    }

    void LateUpdate()
    {
        if (isFollowing)
        {
            // Apply that _followOffset to get a target position
            Vector3 targetPosition = player.position + followOffset;

            // Smooth follow 
            transform.position += (targetPosition - transform.position) * followSharpness;

            // Smooth rotation
            // get a rotation that points Z axis forward, and the Y axis towards the target
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (player.position - transform.position));
            // rotate toward the target rotation, never rotating farther than "lookSpeed" in one frame.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }

        if (hasFoundGip)
        {

        }
    }
}
