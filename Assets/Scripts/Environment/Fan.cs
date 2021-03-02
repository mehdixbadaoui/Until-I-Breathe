using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float air;
    public float capacity;

    public GameObject door;
    Vector3 initial_door_pos;
    public float slide_height = 1f;
    public float slide_speed = .5f;

    public GameObject generator;


    // Start is called before the first frame update
    void Start()
    {
        air = 0;
        initial_door_pos = door.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (air >= capacity)
        {
            //MOVE DOOR
            door.transform.position = Vector3.Lerp(door.transform.position, initial_door_pos + Vector3.up * slide_height, slide_speed);

            //TURN ON GENERATOR
            //NOTHING YET
        }

    }

    public void incAir(float amount)
    {
        air += amount;

    }
}
