using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float air;
    public float capacity;

    public List<GameObject> doors;
    public GameObject generator;
    public List<GameObject> lights;


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
            //UNLOCK THE DOORS
            foreach (GameObject door in doors)
                door.GetComponentInChildren<Door>().locked = false;

            //TURN ON GENERATOR
            //NOTHING YET

            //TURN LIGHTS TO GREEN
            foreach(GameObject light in lights)
                light.GetComponent<Light>().color = Color.green;
        }

    }

    public void incAir(float amount)
    {
        if(air <= capacity)
            air += amount;

    }
}
