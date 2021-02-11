using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimbing : MonoBehaviour
{
    alt_mvt alt_Mvt;
    Rigidbody rb;
    public bool isClimbing = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 rigidbodyPosition = transform.position + new Vector3(0, 1, 0);
        if (Physics.Raycast(rigidbodyPosition, transform.TransformDirection(Vector3.forward), out hit, 1f))
        {
            isClimbing = true;

        }
        else
            isClimbing = false;
        Climbing(isClimbing); 
        Debug.Log(isClimbing); 
    }

    void Climbing(bool isClimbing)
    {
        if ( isClimbing )
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.isKinematic = true; 

            }
        }
    }
}
