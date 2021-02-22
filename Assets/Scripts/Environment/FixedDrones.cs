﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedDrones : MonoBehaviour
{
    // Params of rotation
    [Range(0, 180)]
    public float rotation;
    [Range(0, 1)]
    public float speed;

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
    bool detected = false;

    private void Start()
    {
        //keeps track of the coroutine created
        IEnumerator coOR = ObjectRotate();
        StartCoroutine(coOR);
    }

    private void Update()
    {
        //keeps track of the coroutine created
        IEnumerator coFT = FindTargetWithDelay();
        StartCoroutine(coFT);
    }

    IEnumerator ObjectRotate()
    {
        float timer = 0;
        while (!detected)
        {
            float angle = Mathf.Sin(timer) * rotation;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

            timer += Time.deltaTime * speed;
            yield return null;
        }
    }

    IEnumerator FindTargetWithDelay()
    {
        while (true)
        {
            yield return null; //waits for one frame
            FindVisibleTarget();
        }
    }

    // Responsible for finding the target in view
    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacletMask)) //actions to add if player is detected
                {
                    visibleTargets.Add(target); //keeps track of the visible target in an array
                    detected = true;
                    light.color = Color.red;
                }
            }
        }

    }

    // Does the conversion automatically and fetches a direction in degrees depending on the angle
    public Vector3 GetDirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
