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
    private CheckLenghtSound checkLenghtSound;
    private GameObject uni; 
    private bool isIncAir = false;
    public Vector3 distanceVentilatorUni;
    public float maxDistanceFromVentilator = 7f;
    private float ventilatorVolume;
   


    // Start is called before the first frame update
    void Start()
    {
        air = 0;
        uni = GameObject.FindGameObjectWithTag("uni");
        checkLenghtSound = uni.GetComponent<CheckLenghtSound>();
        // Get the fan anim
        fanAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (air >= capacity)
        {
            fanAnim.SetBool("isrotating", true);
            bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject("Little_Ventilator_event", this.gameObject);
            if (!isSoundFinished)
                AkSoundEngine.PostEvent("Little_Ventilator_event", this.gameObject);
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
            foreach(GameObject light in lights)
                light.GetComponentInChildren<Light>().color = Color.green;
        }
        distanceVentilatorUni = new Vector3(0, 0, this.gameObject.transform.position.z - uni.transform.position.z);
        RTPCVentilatorSound(distanceVentilatorUni);


    }
    private void RTPCVentilatorSound(Vector3 dstVentilatorUni)
    {

        
        if (dstVentilatorUni.z <= maxDistanceFromVentilator && dstVentilatorUni.z > 0)
        {
            ventilatorVolume = Mathf.Abs(100 - distanceVentilatorUni.z * 100f / maxDistanceFromVentilator) * fanAnim.speed;
            AkSoundEngine.SetRTPCValue("VentilatorVolume", ventilatorVolume);
        }
        else if (dstVentilatorUni.z >= -maxDistanceFromVentilator && dstVentilatorUni.z < 0)
        {
            ventilatorVolume = (100 - Mathf.Abs(distanceVentilatorUni.z * 100f / maxDistanceFromVentilator)) * fanAnim.speed;
            AkSoundEngine.SetRTPCValue("VentilatorVolume",  ventilatorVolume);
        }
        else
        {
            ventilatorVolume = 0f;
            AkSoundEngine.SetRTPCValue("VentilatorVolume", 0f);
        }
    }

    private void FixedUpdate()
    {
        if (!isIncAir && air < capacity)
        {
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
            bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject("Little_Ventilator_event", this.gameObject);
            AkSoundEngine.SetRTPCValue("VentilatorVolume", ventilatorVolume);
            if (!isSoundFinished)
                AkSoundEngine.PostEvent("Little_Ventilator_event", this.gameObject);

            isIncAir = true;
            air += amount;
            fanAnim.SetBool("isrotating", true);
            fanAnim.speed = Mathf.Lerp(fanAnim.speed, air/2 , Time.deltaTime);
            
        }
        
    }
}
