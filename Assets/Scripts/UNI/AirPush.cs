using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPush : MonoBehaviour
{

    public float flyForce;

    private bool isFlying = false;

    private CapsuleCollider collider;

    private GameObject uniGO;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>() ;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (isFlying)
        {
            uniGO.GetComponent<Rigidbody>().AddForce(new Vector3(0, flyForce * 1/( (uniGO.transform.position.y - collider.bounds.min.y) / (collider.bounds.size.y)) , 0), ForceMode.Force);
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "uni")
        {
            uniGO = other.gameObject;
            uniGO.GetComponent<Movement>().IsFlying = true;
            isFlying = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "uni")
        {
            isFlying = false;
            uniGO.GetComponent<Movement>().IsFlying = false;
            //isJumping

        }
    }
}
