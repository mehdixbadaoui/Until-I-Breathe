using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook_detecto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "hook") Debug.Log("CAN HOOK");
        //Debug.Log(col.name);
    }

}
