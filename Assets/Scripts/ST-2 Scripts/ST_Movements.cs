using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST_Movements : MonoBehaviour
{
    public Transform Player;
    public float followSharpness = 0.05f;
    public float rotationSpeed = 5f;

    Vector3 followOffset;
    Vector3 startOffset;
    Vector3 rotationMask;

    private hook_detector HookDetector;

    // HINTS

    bool nearHint;
    Transform hintPosition;

    void Start()
    {
        Vector3 rotationMask = new Vector3(0, 1, 0);

        // Initiliaze the start position of ST-2
        Vector3 startOffset = new Vector3(-2.5f, 2f, 0.0f);
        // Initiliaze the position of ST-2 at the start of  the game
        transform.position = Player.position + startOffset;

        // Cache the initial offset at time of load/spawn
        followOffset = transform.position - Player.transform.position;

        // Fetches the script Hook_Detector from the Player GO
        HookDetector = Player.GetComponent<hook_detector>();

    }

    public void Update()
    {     
    }

    void LateUpdate()
    {
        // Allows ST-2 to follow the player
        if (!HookDetector.nearHook && !nearHint)
        {
            // Apply that followOffset to get a target position
            Vector3 targetPosition = Player.position + followOffset;

            // Smooth follow 
            transform.position += (targetPosition - transform.position) * followSharpness;

            // Smooth rotation
            // get a rotation that points Z axis forward, and the Y axis towards the target
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (Player.position - transform.position));
            // rotate toward the target rotation, never rotating farther than "lookSpeed" in one frame.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
        else // Allows ST-2 to show the nearest hook to the player
        {
            Vector3 desiredPosition = (HookDetector.nearest_hook.transform.position + (transform.position - HookDetector.nearest_hook.transform.position).normalized * 2f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSharpness);

            transform.position = smoothedPosition;

            transform.RotateAround(HookDetector.nearest_hook.transform.position, Vector3.right, 180.0f * Time.deltaTime);
        }

        // Allows ST-2 to show the nearest hint to the player
        if (!HookDetector.nearHook && HookDetector.nearHint)
        {
            Vector3 desiredPosition = (HookDetector.hintPosition.position + (transform.position - HookDetector.hintPosition.position).normalized * 1f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSharpness);

            transform.position = smoothedPosition;
        }
    }
}
