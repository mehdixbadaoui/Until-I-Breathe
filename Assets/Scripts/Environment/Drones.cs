using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones : MonoBehaviour
{
    [Range(5, 25)]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacletMask;

    public List<Transform> targetsInView = new List<Transform>();

    public Transform pointB;
    public float timeItTakes;
    public int pause;

    [Range(0f, 1f)]
    public float speed;

    IEnumerator Start()
    {
        StartCoroutine(FindTargetWithDelay());

        //infinite loop
        var pointA = transform.position; //initial position of the drone is the start
        while (true)
        {
            // From pA -> pB
            yield return StartCoroutine(MoveObject(transform, pointA, pointB.position, timeItTakes));
            var desiredRotQA = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotQA, 1f);
            yield return new WaitForSeconds(pause);
            

            // From pB -> pA
            yield return StartCoroutine(MoveObject(transform, pointB.position, pointA, timeItTakes));
            var desiredRotQB = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotQB, 1f);
            yield return new WaitForSeconds(pause);
        }
    }

    // Actually responsible for moving the drone
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
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
            yield return new WaitForSeconds(2);
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {
        targetsInView.Clear();
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
                    //Attack
                    targetsInView.Add(target);
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
