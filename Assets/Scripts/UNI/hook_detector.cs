using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class hook_detector : MonoBehaviour
{
    public GameObject nearest_hook;
    public GameObject player;
    public List<GameObject> all_hooks;

    public bool nearHook = false;

    //HINTS & DEAD ROBOTS
    public bool nearHint = false;
    public bool nearDead = false;
    public Transform hintPosition;
    public Transform deadPosition;

    // Start is called before the first frame update
    void Start()
    {
        all_hooks = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("uni");
    }

    // Update is called once per frame
    void Update()
    {
        if (all_hooks.Count != 0)
        {
            //CHOOSE THE NEAREST HOOK TO THE PLAYER
            //nearest_hook = all_hooks.OrderBy(o => Vector3.Distance(Camera.main.WorldToScreenPoint((o.transform.position), Camera.main.WorldToScreenPoint(Input.mousePosition))).ToList()[0];

            //CHOOSE THE NEAREST HOOK TO THE CURSOR
            nearest_hook = all_hooks.OrderBy(o => Vector3.Distance(Camera.main.WorldToScreenPoint(o.transform.position), Input.mousePosition)).ToList()[0];
        }
        else
            nearest_hook = null;


        //CHANGE THE APPEARANCE OF THE SELECTED HOOK
        foreach(var h in all_hooks)
        {
            if(h == nearest_hook) h.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            else h.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
        }
    }

    public GameObject nh()
    {
        return nearest_hook;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!all_hooks.Contains(other.gameObject) && (other.tag == "hook" || other.tag == "movable_hook" || other.tag == "lever") )
        {
            all_hooks.Add(other.gameObject);
            nearHook = true;
        }
        else

        // HINTS
        if (other.tag == "Hint")
        {
            hintPosition = other.gameObject.transform;
            nearHint = true;
        }

        // DEAD ROBOT
        if (other.tag == "Dead")
        {
            deadPosition = other.gameObject.transform;
            nearDead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (all_hooks.Contains(other.gameObject))
        {
            all_hooks.Remove(other.gameObject);
            other.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
            nearHook = false;
        }

        // HINTS
        if (other.tag == "Hint")
        {
            nearHint = false;
        }

        //DEAD
        if (other.tag == "Dead")
        {
            nearDead = false;
        }
    }
}
