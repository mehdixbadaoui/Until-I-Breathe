﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class hook_detector : MonoBehaviour
{
    public GameObject nearest_hook;
    public GameObject player;
    private List<GameObject> all_hooks;

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
            nearest_hook = all_hooks.OrderBy(o => Vector3.Distance(o.transform.position, player.transform.position)).ToList()[0];
        }
        else
            nearest_hook = null;
    }

    public GameObject nh()
    {
        return nearest_hook;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!all_hooks.Contains(other.gameObject) && (other.tag == "hook" || other.tag == "movable_hook") )
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
