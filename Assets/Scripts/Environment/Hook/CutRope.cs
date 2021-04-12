using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour
{

    public bool untag = false;
    public Checkpoint CheckpointAfter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("uni"))
        {
            Debug.Log("UNI");
        }

            if ( other.CompareTag("hook") || other.CompareTag("movable_hook") )
        {
            Debug.Log("tag Done");
            Debug.Log(GameObject.FindGameObjectWithTag("uni").GetComponent<GrapplingHook>().hookObject.name);
            if ( GameObject.FindGameObjectWithTag("uni").GetComponent<GrapplingHook>().hookObject == other.gameObject && (CheckpointAfter == null || !CheckpointAfter.alreadyChecked) )
            {
                GameObject.FindGameObjectWithTag("uni").GetComponent<GrapplingHook>().CutRope();
                if (untag)
                    other.tag = "Untagged";
            }
        }
    }
}
