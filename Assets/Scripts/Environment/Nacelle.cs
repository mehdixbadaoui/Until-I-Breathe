using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacelle : MonoBehaviour
{
    public float WaitTime;
    public float speed;
    public GameObject sol;

    public Transform leftDoor, rightDoor;

    public List<Transform> targets; 
    float time;
    int index;
    public bool move = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            StartCoroutine(MoveDoor(leftDoor, Vector3.right));
            StartCoroutine(StartMoving(WaitTime));
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
            Vector3 initialPos = transform.position;
            if (transform.position != targets[index].position)
                transform.position = Vector3.MoveTowards(transform.position, targets[index].position, speed);
            else
                index++;
        }
        if (index == targets.Count && sol)
        {
            sol.GetComponent<BoxCollider>().enabled = false;
            rightDoor.position -= Vector3.right * Time.deltaTime * .5f;
        }
    }

    IEnumerator StartMoving(float time)
    {
        yield return new WaitForSeconds(time);
        index = 0;
        move = true;
        yield return null;
    }

    IEnumerator MoveDoor(Transform door, Vector3 direction)
    {
        float time = Time.time;

        while (Time.time < time + .5f)
        {
            door.position += direction / (Time.time - time);
        }
        yield return null;
    }
}
