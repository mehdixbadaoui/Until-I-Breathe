using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class hook_detector : MonoBehaviour
{
    public GameObject nearest_hook;
    public GameObject player;
    private List<GameObject> all_hooks;

    [Range(1f, 10f)]
    public float detection_radius = 5f;

    public bool nearHook = false;

    //HINTS
    public bool nearHint = false;
    public Transform hintPosition;

    // Start is called before the first frame update
    void Start()
    {
        all_hooks = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("uni");
    }

    // Update is called once per frame
    void Update()
    {
        //List<Order> SortedList = objListOrder.OrderBy(o => o.OrderDate).ToList();

        gameObject.GetComponent<SphereCollider>().radius = detection_radius;
        if (all_hooks.Count != 0)
        {
            nearest_hook = all_hooks.OrderBy(o => Vector3.Distance(o.transform.position, player.transform.position)).ToList()[0];
            //Debug.Log(nearest_hook);
        }
        //else 
        //    Debug.Log("null");
    }

    public GameObject nh()
    {
        return nearest_hook;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!all_hooks.Contains(other.gameObject) && other.tag == "hook")
        {
            all_hooks.Add(other.gameObject);
            nearHook = true;
        }


        // HINTS
        if (other.tag == "Hint")
        {
            hintPosition = other.gameObject.transform;
            nearHint = true;
        }
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.tag == "hook") Debug.Log("CAN HOOK");

    //}

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
    }
}
