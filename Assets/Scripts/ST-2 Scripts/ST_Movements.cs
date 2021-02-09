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

    // To access the HookDetector_Script
    private hook_detector HookDetector;

    // To access the children components of ST-2
    public GameObject ChildGO_Sprite;
    private Vector3 rotation_Sprite;
    public Sprite[] sprites;

    void Start()
    {
        // Stores the initial rotation of the sprite component
        rotation_Sprite = ChildGO_Sprite.transform.rotation.eulerAngles;

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
    
    private void Update()
    {
        // Prevents the sprite from rotating on the Z axis
        rotation_Sprite.z = Mathf.Clamp(rotation_Sprite.z, 0f, 0f);
        ChildGO_Sprite.transform.rotation = Quaternion.Euler(rotation_Sprite);
    }

    void LateUpdate()
    {
        // Allows ST-2 to follow the player
        if (!HookDetector.nearHook && !HookDetector.nearDead && !HookDetector.nearHint)
        {
            // Resets expression
            ChildGO_Sprite.GetComponent<SpriteRenderer>().sprite = sprites[0];

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
        if (HookDetector.nearHook && !HookDetector.nearDead && !HookDetector.nearHint) // Allows ST-2 to show the nearest HOOK to the player
        {
            // Changes expression
            ChildGO_Sprite.GetComponent<SpriteRenderer>().sprite = sprites[1];

            Vector3 desiredPosition = (HookDetector.nearest_hook.transform.position + (transform.position - HookDetector.nearest_hook.transform.position).normalized * 2f);
            // Smoothes the path between the initial and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSharpness);

            transform.position = smoothedPosition;

            // Rotates near the object of interest
            transform.RotateAround(HookDetector.nearest_hook.transform.position, Vector3.right, 180.0f * Time.deltaTime);
        }

        // Allows ST-2 to show the nearest HINT to the player
        if (!HookDetector.nearHook && !HookDetector.nearDead && HookDetector.nearHint)
        {
            Vector3 desiredPosition = (HookDetector.hintPosition.position + (transform.position - HookDetector.hintPosition.position).normalized * 2f);
            // Smoothes the path between the initial and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSharpness);

            transform.position = smoothedPosition;

            // Rotates near the object of interest
            transform.RotateAround(HookDetector.hintPosition.position, Vector3.up, 180.0f * Time.deltaTime);
        }

        // Allows ST-2 to collect lore from dead robots nearby
        if (!HookDetector.nearHook && !HookDetector.nearHint && HookDetector.nearDead)
        {
            Vector3 desiredPosition = (HookDetector.deadPosition.position + (transform.position - HookDetector.deadPosition.position).normalized * 1f);
            // Smoothes the path between the initial and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSharpness);

            // Rotates near the object of interest
            transform.position = smoothedPosition;

            // add features relative to HUD of lore found on dead robots
            ChildGO_Sprite.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
    }
}
