using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float wait_time = 1f;


    private IEnumerator OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "uni")
        {
            yield return new WaitForSecondsRealtime(wait_time);

            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;

            yield return new WaitForSeconds(5);
            Destroy(gameObject);

        }

    }
}
