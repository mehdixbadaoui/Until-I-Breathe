using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public Transform pointB;
    Vector3 pointA;
    public float timeItTakes;
    public int pause;

    public GameObject PlayerParent;
    public GameObject Platform;

    void Start()
    {
        //initial position of the platform is the start
        pointA = transform.position;
        StartCoroutine(ChangeDir());
    }

    IEnumerator ChangeDir()
    {
        //infinite loop
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB.position, timeItTakes));
            yield return new WaitForSeconds(pause);
            yield return StartCoroutine(MoveObject(transform, pointB.position, pointA, timeItTakes));
            yield return new WaitForSeconds(pause);
        }
    }

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

    // Allows the player and other objects to stick to the platform and move on it
    private void OnTriggerEnter(Collider other)
    {
        PlayerParent.transform.parent = Platform.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerParent.transform.parent = null;
    }
}
