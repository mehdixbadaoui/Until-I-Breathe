using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilatorSound : MonoBehaviour
{
    private GameObject uni;
    public GameObject ventilator;
    public Vector3 distanceVentilatorUni;
    public Vector3 distanceVentilatorUniNormalized;
    public Vector3 distanceVentilatorUniNormalizedfoiscent;


    // Start is called before the first frame update
    void Start()
    {
        uni = GameObject.FindGameObjectWithTag("uni");
        ventilator = this.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        distanceVentilatorUni = new Vector3(0,0,ventilator.transform.position.z - uni.transform.position.z);
        distanceVentilatorUniNormalized = distanceVentilatorUni.normalized;
        distanceVentilatorUniNormalizedfoiscent = distanceVentilatorUniNormalized * 100;

        RTPCVentilatorSound(distanceVentilatorUni);

    }
    private void RTPCVentilatorSound(Vector3 dstVentilatorUni)
    {
        if(dstVentilatorUni.z <= 15 && dstVentilatorUni.z > 0)
        {

        }
    }
}
