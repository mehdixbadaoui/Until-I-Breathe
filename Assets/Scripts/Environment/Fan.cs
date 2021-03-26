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

    private Animator fanAnim;
    private bool isIncAir = false;


    // Start is called before the first frame update
    void Start()
    {
        air = 0;

        // Get the fan anim
        fanAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (air >= capacity)
        {
            fanAnim.SetBool("isrotating", true);
            fanAnim.speed = Mathf.Lerp(0, 1, Time.deltaTime);
            //UNLOCK THE DOOR
            if (door)
                door.GetComponentInChildren<Door>().locked = false;

            //TURN ON GENERATOR
            //NOTHING YET

            //TURN LIGHT TO GREEN
            if (light)
                light.GetComponent<Light>().color = Color.green;
        }


    }

    private void FixedUpdate()
    {
        if (!isIncAir && air < capacity)
        {
            fanAnim.speed = Mathf.Lerp(fanAnim.speed, 0, Time.deltaTime);
            if (fanAnim.speed < 0.1)
                fanAnim.SetBool("isrotating", false);
        }
        else if (isIncAir)
        {
            isIncAir = false;
        }
    }

    public void incAir(float amount)
    {
        if (air <= capacity)
        {
            isIncAir = true;
            air += amount;d
            fanAnim.SetBool("isrotating", true);
            fanAnim.speed = Mathf.Lerp(fanAnim.speed, air/2 , Time.deltaTime);
        }
        
    }
}
