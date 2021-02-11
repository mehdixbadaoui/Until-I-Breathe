using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "ground")
            alt_mvt.isGrounded = true;

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "ground")
            alt_mvt.isGrounded = false;

    }

    private void Update()
    {
        //Debug.Log(alt_mvt.can_jump);
    }

}
