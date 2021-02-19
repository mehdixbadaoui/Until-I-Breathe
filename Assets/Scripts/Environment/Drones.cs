using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones : MonoBehaviour
{
    // Params of patrolling
    [Range(5, 25)]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacletMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    // Params of light&detection
    public Light light;
    bool detected;
    bool canTurn;


    // Params of moving
    public Transform pointB;
    Vector3 pointA;
    public float timeItTakes;
    public int pause;

    [Range(0f, 10f)]
    public float speed;

    void Start()
    {
        //initial position of the drone is the start
        pointA = transform.position;
        canTurn = true;

        IEnumerator coDR = ChangeDir();
        StartCoroutine(coDR);
    }

    void Update()
    {


        IEnumerator coFT = FindTargetWithDelay();
        StartCoroutine(coFT);

        //if(detected)
        //{
        //    StopCoroutine(coFT);
        //    StopCoroutine(coDR);
        //}
    }

    IEnumerator ChangeDir()
    {
        while(true)
        {
        // From pA -> pB
        yield return StartCoroutine(MoveObject(transform, pointA, pointB.position, timeItTakes));
        if (canTurn) //if player detected cant turn
        {
            var desiredRotQA = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotQA, 1f);
            yield return new WaitForSeconds(pause);
        }


        // From pB -> pA
        yield return StartCoroutine(MoveObject(transform, pointB.position, pointA, timeItTakes));
        if (canTurn) //if player detected cant turn
        {
            var desiredRotQB = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotQB, 1f);
            yield return new WaitForSeconds(pause);
        }
        }

        
    }

    // Actually responsible for moving the drone
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f && !detected)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);

            yield return null;
        }
    }

    IEnumerator FindTargetWithDelay()
    {
        while(true)
        {
            yield return null;
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i =0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
                
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacletMask))
                {
                    visibleTargets.Add(target);
                    detected = true;
                    canTurn = false;
                    light.color = Color.red;
                }
            }
        }

    }

    // Does the conversion automatically and fetches a direction depending on the angle
    public Vector3 GetDirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
