using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "uni") facade.SetActive(!facade.active);
    }

}
