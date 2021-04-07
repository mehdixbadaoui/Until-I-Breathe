using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacelle : MonoBehaviour
{
    public Transform target;
    public float WaitTime;
    public float travelTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            StartCoroutine(GoUp());
        }
    }

    IEnumerator GoUp()
    {
        yield return new WaitForSeconds(WaitTime);
        transform.position = Vector3.MoveTowards(transform.position, target.position, travelTime);
    }
}
