using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilatorSound : MonoBehaviour
{
    private GameObject uni;
    private GameObject ventilator;
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
        
       
    }
    private void FixedUpdate()
    {
        distanceVentilatorUni = playEventWithRTPCSound.CalculateDistanceUniFromObject(this.transform.position);

       
        playEventWithRTPCSound.RTPCGameObjectValue(distanceVentilatorUni, maxDistanceFromVentilator, this.gameObject, "Big_Ventilator_event" , "VentilatorVolume");


    }

}
