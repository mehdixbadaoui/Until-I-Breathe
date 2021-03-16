using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST_Movements : MonoBehaviour
{
    public Transform Player;
    public float followSharpness = 2f;
    public float rotationSpeed = 5f;

    public Vector3 followOffset;
    Vector3 startOffset;
    Vector3 rotationMask;

    [Range(0.2f, 10f)]
    public float fixationSpeed;
    [Range(0.5f, 10f)]
    public float distFromObj;
    [Range(1f, 10f)]
    public float lookAtSpeed;

    Vector3 rotationOffset; //optional, not used right now

    private hook_detector HookDetector;

    // To access the children components of ST-2 (sprite)
    //public GameObject ChildGO_Sprite;
    //private Vector3 rotation_Sprite;
    //public Sprite[] sprites;

    void Start()
    {
        // Automatically find player
        Player = GameObject.FindGameObjectWithTag("uni").transform;

        // Stores the initial rotation of the sprite component
        //rotation_Sprite = ChildGO_Sprite.transform.rotation.eulerAngles;

        Vector3 rotationMask = new Vector3(0, 1, 0);

        // Initialize the start position of ST-2
        Vector3 startOffset = new Vector3(-2.5f, 2f, 0.0f);

        // Initialize the position of ST-2 at the start of  the game
        transform.position = Player.position + startOffset;

        // Cache the initial offset at time of load/spawn
        //followOffset = transform.position - Player.transform.position;

        // Fetches the script Hook_Detector from the Player GO
        HookDetector = Player.Find("hook_detector").GetComponent<hook_detector>();
    }

    private void Update()
    {
        // Prevents the sprite from rotating on the Z axis
        //rotation_Sprite.z = Mathf.Clamp(rotation_Sprite.z, 0f, 0f);
        //ChildGO_Sprite.transform.rotation = Quaternion.Euler(rotation_Sprite);
    }

    void FixedUpdate()
    {
        // Allows ST-2 to follow the player
        if (!HookDetector.nearHook && !HookDetector.nearDead && !HookDetector.nearHint)
        {
            // Resets expression
            //ChildGO_Sprite.GetComponent<SpriteRenderer>().sprite = sprites[0];

            //TO ALWAYS STAY BEHIND UNI
            Vector3 _followOffset = Player.transform.TransformDirection(-Vector3.one * Player.transform.localScale.z);
            _followOffset.x = followOffset.x;
            _followOffset.y = followOffset.y;
            _followOffset.z *= followOffset.z;

            // Apply that followOffset to get a target position
            Vector3 targetPosition = Player.position + _followOffset;

            // Smooth follow (Multiple Methods)
            transform.position += (targetPosition - transform.position) * followSharpness * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position + followOffset, followSharpness);


            // Smooth rotation
            // get a rotation that points Z axis forward, and the Y axis towards the target
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (Player.position - transform.position));
            // rotate toward the target rotation, never rotating farther than "rotationSpeed" in one frame.
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            transform.LookAt(Player.Find("ST2 Follow"));
        }
        
        // Allows ST-2 to show the nearest HOOK to the player
        if (HookDetector.nearHook && !HookDetector.nearDead && !HookDetector.nearHint) 
        {
            // Changes expression
            //ChildGO_Sprite.GetComponent<SpriteRenderer>().sprite = sprites[1];

            if (HookDetector.nearest_hook != null)
            {
                Vector3 desiredPosition = HookDetector.nearest_hook.transform.position + ((transform.position - HookDetector.nearest_hook.transform.position).normalized * distFromObj);
                // Smooths the path between the initial and desired position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, fixationSpeed * Time.deltaTime);
                transform.position = smoothedPosition;

                if (Math.Abs(transform.position.z - desiredPosition.z) < 1.5f) //check if ST2 is near enough from the hook and if so look at player otherwise it means it's still heading there
                {
                    SmoothLookAt(Player.Find("ST2 Follow").transform.position);
                }
            }
            
            // Rotates near the object of interest
            //transform.RotateAround(HookDetector.nearest_hook.transform.position, Vector3.right, 180.0f * Time.deltaTime);
        }

        // Allows ST-2 to show the nearest HINT to the player
        if (!HookDetector.nearHook && !HookDetector.nearDead && HookDetector.nearHint)
        {
            Vector3 desiredPosition = (HookDetector.hintPosition.position + (transform.position - HookDetector.hintPosition.position).normalized * distFromObj);
            // Smooths the path between the initial and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, fixationSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            if (Math.Abs(transform.position.z - desiredPosition.z) < 1f) //check if ST2 is near enough from the hint and if so look at player otherwise it means it's still heading there
            {
                SmoothLookAt(Player.Find("ST2 Follow").transform.position);
            }

            // Rotates near the object of interest
            //transform.RotateAround(HookDetector.hintPosition.position, Vector3.up, 180.0f * Time.deltaTime);
        }

        // Allows ST-2 to collect lore from dead robots nearby
        if (!HookDetector.nearHook && !HookDetector.nearHint && HookDetector.nearDead)
        {
            Vector3 desiredPosition = (HookDetector.deadPosition.position + (transform.position - HookDetector.deadPosition.position).normalized * distFromObj);
            // Smooths the path between the initial and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, fixationSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            if (Math.Abs(transform.position.z - desiredPosition.z) < 1f) //check if ST2 is near enough from the dead robot and if so look at player otherwise it means it's still heading there
            {
                SmoothLookAt(Player.Find("ST2 Follow").transform.position);
            }

            // Rotates near the object of interest
            //transform.RotateAround(HookDetector.hintPosition.position, Vector3.up, 180.0f * Time.deltaTime);

            // Add features relative to HUD of lore found on dead robots and change expression to something sad/confused??
            //ChildGO_Sprite.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
    }

    //To look at the player smoothly after heading to a different location (different than the initial offset)
    void SmoothLookAt(Vector3 newDirection)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newDirection - transform.position), lookAtSpeed * Time.deltaTime);
    }
}
