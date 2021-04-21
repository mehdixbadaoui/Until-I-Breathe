using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacelle : MonoBehaviour
{
    public float WaitTime;
    public float speed;
    public GameObject sol;

    public Transform leftDoor, rightDoor;

    public float floorSpeed = .1f;
    public List<Transform> targets; 
    float time;
    int index;
    public bool move = false;

    private Vector3 oldpos;
    private bool openrightdoor = true;
    private bool openleftdoor = true;
    private bool openfloor = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            if (openleftdoor)
            {
                StartCoroutine(MoveDoor(leftDoor, Vector3.left));
                openleftdoor = false;
            }
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

        else if(index == targets.Count)
        {
            if(rightDoor)
                oldpos = rightDoor.position;

            if (sol && openfloor)
            {
                if (sol.transform.eulerAngles != new Vector3(90, 0, 0))
                    sol.transform.eulerAngles = Vector3.Lerp(sol.transform.eulerAngles, new Vector3(90, 0, 0), floorSpeed);
            }
            else if (openrightdoor && openrightdoor)
            {
                StartCoroutine(MoveDoor(rightDoor, Vector3.right));
                openrightdoor = false;
            }
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

        Vector3 Gotoposition = door.position + direction;
        float elapsedTime = 0;
        float waitTime = 1f;

        Vector3 currentPos = door.position;

        while (elapsedTime < waitTime)
        {
            door.position = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        door.position = Gotoposition;
        yield return null;

    }
}
