using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBalloon : MonoBehaviour
{
    public Transform startPosition;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("blowable"))
        {
            collision.collider.transform.position = startPosition.position;
            collision.collider.GetComponent<ballon>().air = 0f;

        }

    }
}
