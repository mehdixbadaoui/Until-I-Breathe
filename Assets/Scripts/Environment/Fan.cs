using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float air;
    public float capacity;
    public bool isOn = false;

    public List<GameObject> doors;
    public GameObject generator;
    public List<GameObject> lights;

    private Animator fanAnim;
    private DistanceUniFromObjects distanceUniFromObjects;
    
    private GameObject uni; 
    private bool isIncAir = false;
    public float maxDistanceFromVentilator = 7f;
    private Vector3 distFanFromUni; 


    // Start is called before the first frame update
    void Start()
    {
        air = 0;
        uni = GameObject.FindGameObjectWithTag("uni");
        distanceUniFromObjects = uni.GetComponent<DistanceUniFromObjects>();
        
        // Get the fan anim
        fanAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distFanFromUni = distanceUniFromObjects.CalculateDistanceUniFromObject(this.gameObject.transform.position);
        if (air >= capacity)
        {
            
            distanceUniFromObjects.RTPCGameObjectValue(distFanFromUni, maxDistanceFromVentilator,this.gameObject, "Little_Ventilator_event", "FanVolume", fanAnim.speed);

            if (!isOn)
            {
                isOn = true;
                fanAnim.SetBool("isrotating", true);
                fanAnim.speed = 1;

                //UNLOCK THE DOOR
                foreach (GameObject door in doors)
                    door.GetComponentInChildren<Door>().locked = false;

                //TURN ON GENERATOR
                if (generator)
                {
                    generator.GetComponent<Renderer>().material.SetColor("_EmissiveColor", new Color(80000, 80000, 80000, 80000));
                }

                //TURN LIGHTS TO GREEN
                foreach (GameObject light in lights)
                    light.GetComponentInChildren<Light>().color = Color.green;
            }
        }
        


    }
   

    private void FixedUpdate()
    {
        if (!isIncAir && air < capacity)
        {
            distanceUniFromObjects.RTPCGameObjectValue(distFanFromUni, maxDistanceFromVentilator, this.gameObject, "Little_Ventilator_event", "FanVolume", fanAnim.speed);
            fanAnim.speed = Mathf.Lerp(fanAnim.speed, 0, Time.deltaTime);
            if (fanAnim.speed < 0.1)
            {
                fanAnim.SetBool("isrotating", false);
                
            }
                
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
