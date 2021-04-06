using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Train : MonoBehaviour
{
    public GameObject track;
    private List<Transform> targets;
    private int i = 0;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        targets = track.GetComponentsInChildren<Transform>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targets[i].position, speed * Time.deltaTime);
        if(transform.position == targets[i].position && i < targets.Count - 1)
        {
            i++;
            transform.LookAt(targets[i]);
            //transform.LookAt(Vector3.Lerp(targets[i-1].position, targets[i].position, .1f));
        }
    }

}
