using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    private bool visible;

    private void Start()
    {
        visible = true;
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "uni") /*fade(visible);*/ facade.SetActive(!facade.active);
    }

    private void fade(bool visible)
    {
        Vector3 initial_pos = facade.transform.position;
        facade.transform.position = Vector3.Lerp(initial_pos, initial_pos + new Vector3(7, 0, 0), 20f);
    }
}
