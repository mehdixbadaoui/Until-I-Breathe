using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float wait_time = 1f;

    private Vector3 lastPos;
    private Quaternion lastRot;
    private Vector3 lastScale;

    void Start()
    {

        //Initialize the last pos
        lastPos = transform.position;

        //Initialize the last pos
        lastRot = transform.rotation;

        //Initialize the last pos
        lastScale = transform.localScale;


    }

    public void Respawn()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = lastPos;
        transform.rotation = lastRot;
        transform.localScale = lastScale;
        enabled = true;
    }

    private IEnumerator OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "uni")
        {
            yield return new WaitForSecondsRealtime(wait_time);

            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;

        }


        else if (col.gameObject.GetComponent<DeadZone>() != null )
        {
            yield return new WaitForSecondsRealtime(wait_time);

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            enabled = false;
        }

    }
}
