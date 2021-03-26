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
            fanAnim.speed = 1;
            //UNLOCK THE DOOR
            foreach (GameObject door in doors)
                door.GetComponentInChildren<Door>().locked = false;

            //TURN ON GENERATOR
            //NOTHING YET

            //TURN LIGHTS TO GREEN
            foreach(GameObject light in lights)
                light.GetComponentInChildren<Light>().color = Color.green;
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
            air += amount;
            fanAnim.SetBool("isrotating", true);
            fanAnim.speed = Mathf.Lerp(fanAnim.speed, air/2 , Time.deltaTime);
        }
        
    }
}
