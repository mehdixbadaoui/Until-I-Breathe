using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float air;
    public float capacity;

    public GameObject door;
    public GameObject generator;
    public GameObject light;


    // Start is called before the first frame update
    void Start()
    {
        air = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (air >= capacity)
        {
            //UNLOCK THE DOOR
            if(door)
                door.GetComponentInChildren<Door>().locked = false;

            //TURN ON GENERATOR
            //NOTHING YET

            //TURN LIGHT TO GREEN
            if (light)
                light.GetComponent<Light>().color = Color.green;
        }

    }

    public void incAir(float amount)
    {
        if(air <= capacity)
            air += amount;

    }
}
