using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilatorSound : MonoBehaviour
{
    private GameObject uni;
    public GameObject ventilator;
    public Vector3 distanceVentilatorUni;
    public float maxDistanceFromVentilator = 15f; 
    public float ventilatorVolume;
    private DistanceUniFromObjects distanceUniFromObjects;
    
   





    // Start is called before the first frame update
    void Start()
    {
        uni = GameObject.FindGameObjectWithTag("uni");
        distanceUniFromObjects = uni.GetComponent<DistanceUniFromObjects>(); 
       
        ventilator = this.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        
        distanceVentilatorUni = distanceUniFromObjects.CalculateDistanceUniFromObject(ventilator.transform.position); 
        distanceUniFromObjects.RTPCGameObjectValue(distanceVentilatorUni, maxDistanceFromVentilator, ventilator, "Big_Ventilator_event", "VentilatorVolume"); 
        
         
    }
    
}
