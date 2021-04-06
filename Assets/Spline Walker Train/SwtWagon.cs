using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwtWagon : MonoBehaviour
{

    public GameObject TargetConnection;
    public float Offset;
    public float Speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(TargetConnection.transform.position.x, transform.position.y, transform.position.z), Speed * Time.deltaTime);
        transform.LookAt(TargetConnection.transform.position);
            
    }

}
