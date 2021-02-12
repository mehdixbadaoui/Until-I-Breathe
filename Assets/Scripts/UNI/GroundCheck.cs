using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "ground")
            Movement.isGrounded = true;

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "ground")
            Movement.isGrounded = false;

    }

    private void Update()
    {
        //Debug.Log(alt_mvt.can_jump);
    }

}
