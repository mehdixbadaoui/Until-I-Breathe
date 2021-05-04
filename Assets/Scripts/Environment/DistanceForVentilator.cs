using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceForVentilator : MonoBehaviour
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
        ventilator.GetComponent<VentilatorSound>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {
        distanceVentilatorUni = playEventWithRTPCSound.CalculateDistanceUniFromObject(this.transform.position);

        if (Mathf.Abs(distanceVentilatorUni.z) <= maxDistanceFromVentilator && Mathf.Abs(distanceVentilatorUni.y) <= maxDistanceFromVentilator && Mathf.Abs(distanceVentilatorUni.x) <= maxDistanceFromVentilator)
        {
            ventilator.GetComponent<VentilatorSound>().enabled = true;
        }
        else
        {
            ventilator.GetComponent<VentilatorSound>().enabled = false;
        }
       


    }
}
