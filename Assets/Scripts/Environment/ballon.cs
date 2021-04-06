using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballon : MonoBehaviour
{
    public float air;
    public float capacity;
    public float force = 1f;
    //Vector3 initial_position, target_position;

    public float travel_dist;
    public float travel_speed;
    // Start is called before the first frame update
    void Start()
    {
        //air = 0;
        //initial_position = transform.position;
        //target_position = initial_position + Vector3.up * travel_dist;

    }

    // Update is called once per frame
    void Update()
    {
        if(air >= capacity)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target_position, travel_speed);
            transform.Translate(Vector3.up * travel_speed / 10);
            //transform.
        }
    }

    public void incAir(float amount)
    {
        if(air < capacity)
            air += amount;

    }
}
