using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ST_Movements : MonoBehaviour
{
    public Transform Player;
    public float followSharpness = 0.1f;
    public float rotationSpeed = 5f;

    Vector3 followOffset;
    Vector3 startOffset;
    Vector3 rotationMask;

    private hook_detector HookDetector;

    void Start()
    {
        Vector3 rotationMask = new Vector3(0, 1, 0);

        // Initiliaze the start position of ST-2
        Vector3 startOffset = new Vector3(-2.5f, 2f, 0.0f);
        // Initiliaze the position of ST-2 at the start of  the game
        transform.position = Player.position + startOffset;

        // Cache the initial offset at time of load/spawn
        //followOffset = transform.position - Player.transform.position;

        //HookDetector = Player.GetComponent<hook_detector>();
    }

    public void Update()
    {
        //// Calls this func every frame
        //FindClosestEnemy();        
    }

    void LateUpdate()
    {        
        //// Stores the nearest grip
        //GameObject closestGrip = FindClosestEnemy();
        //// Stores the distance between ST-2 & a grip point
        //float dist = Vector3.Distance(Player.position, closestGrip.transform.position);
        
        // Allows ST-2 to follow the player
        if (!HookDetector.nearHook)
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
        else
        {
            for (int i = 0; i < 5; i++)
            {
                //transform.position = closestGrip.transform.position + startOffset;
                transform.position = (HookDetector.nearest_hook.transform.position + (transform.position - HookDetector.nearest_hook.transform.position).normalized * 3f);
                transform.RotateAround(HookDetector.nearest_hook.transform.position, Vector3.up, 50.0f * Time.deltaTime);
            }
        }

        //// Allows ST-2 to show the player where to use the grip
        //if (7 < dist && dist <= 12)
        //{
        //    isFollowing = false;
        //}
    }

    //// Finds the closest grip near ST-2
    //public GameObject FindClosestEnemy()
    //{
    //    GameObject[] gos;
    //    gos = GameObject.FindGameObjectsWithTag("Grip");
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (GameObject go in gos)
    //    {
    //        Vector3 diff = go.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;
    //        if (curDistance < distance)
    //        {
    //            closest = go;
    //            distance = curDistance;
    //        }
    //    }
    //    return closest;
    //}
}
