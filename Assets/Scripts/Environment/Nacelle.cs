using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacelle : MonoBehaviour
{
    public float distance;
    public float WaitTime;
    public float speed;
    public GameObject sol;

    public List<Transform> targets; 
    float time;
    int index;
    public bool move = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            time = 0;
            index = 0;
            move = true;
            other.transform.SetParent(transform);
        }

    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            other.transform.SetParent(null);
        }

    }


    private void FixedUpdate()
    {
        if (move && index < targets.Count)
        {
            if (transform.position != targets[index].position)
                transform.position = Vector3.MoveTowards(transform.position, targets[index].position, speed);
            else
                index++;
        }
    }
}
