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
   

    private CheckLenghtSound checkLenghtSound;



    // Start is called before the first frame update
    void Start()
    {
        uni = GameObject.FindGameObjectWithTag("uni");
        checkLenghtSound = uni.GetComponent<CheckLenghtSound>();
        ventilator = this.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        distanceVentilatorUni = new Vector3(0,0,ventilator.transform.position.z - uni.transform.position.z);
        
        
        RTPCVentilatorSound(distanceVentilatorUni);
         
    }
    private void RTPCVentilatorSound(Vector3 dstVentilatorUni)
    {
        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject("Big_Ventilator_event", ventilator);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent("Big_Ventilator_event", ventilator);
        if (dstVentilatorUni.z <= maxDistanceFromVentilator && dstVentilatorUni.z > 0)
        {
            ventilatorVolume = Mathf.Abs(100 - distanceVentilatorUni.z * 100f / maxDistanceFromVentilator);
            AkSoundEngine.SetRTPCValue("VentilatorVolume", ventilatorVolume);
        }
        else if (dstVentilatorUni.z >= -maxDistanceFromVentilator && dstVentilatorUni.z < 0)
        {
            ventilatorVolume = 100 - Mathf.Abs(distanceVentilatorUni.z * 100f / maxDistanceFromVentilator);
            AkSoundEngine.SetRTPCValue("VentilatorVolume", ventilatorVolume);
        }
        else
        {
            ventilatorVolume = 0f; 
            AkSoundEngine.SetRTPCValue("VentilatorVolume", 0f);
        }
    }
}
