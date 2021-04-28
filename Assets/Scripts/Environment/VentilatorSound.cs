using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilatorSound : MonoBehaviour
{
    private GameObject uni;
    public GameObject ventilator;
    public Vector3 distanceVentilatorUni;
    public float maxDistanceFromVentilator = 15f; 
    
    private PlayEventSounds playEventWithRTPCSound;
    
   





    // Start is called before the first frame update
    void Start()
    {
        uni = GameObject.FindGameObjectWithTag("uni");
        playEventWithRTPCSound = uni.GetComponent<PlayEventSounds>(); 
       
        ventilator = this.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        
        distanceVentilatorUni = playEventWithRTPCSound.CalculateDistanceUniFromObject(ventilator.transform.position); 
        playEventWithRTPCSound.RTPCGameObjectValue(distanceVentilatorUni, maxDistanceFromVentilator, ventilator, "Big_Ventilator_event", "VentilatorVolume"); 
        
         
    }
    
}
