using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("COllision");
        if (col.tag == "uni") facade.SetActive(!facade.active);
    }

    //private void OnCollisionExit(Collision col)
    //{
    //    Debug.Log("COllision");
    //    if (col.collider.tag == "player") facade.SetActive(false);
    //}
}
