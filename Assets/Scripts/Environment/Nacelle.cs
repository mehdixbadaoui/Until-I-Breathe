using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacelle : MonoBehaviour
{
    public float distance;
    public float WaitTime;
    public float speed;
    public GameObject sol;

    float target; 
    float time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            //StartCoroutine(GoUp());
            target = transform.position.y + distance;
            time = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            if (time < WaitTime)
                time += Time.deltaTime;
            //StartCoroutine(GoUp());
            if (transform.position.y < distance && time >= WaitTime)
                transform.position += Vector3.up * speed * Time.deltaTime;
            else if (transform.position.y >= distance && time >= WaitTime)
                sol.GetComponent<BoxCollider>().enabled = false;

        }

    }

    //IEnumerator GoUp()
    //{
    //    yield return new WaitForSeconds(WaitTime);
    //    transform.position = Vector3.MoveTowards(transform.position, target.position, travelTime);
    //}
}
