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
        
        ventilatorVolume = distanceVentilatorUni.z * 100f / 15f;
        RTPCVentilatorSound(distanceVentilatorUni);

    }
    private void RTPCVentilatorSound(Vector3 dstVentilatorUni)
    {
        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject("Ventilator_event", ventilator);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent("Ventilator_event", ventilator);
        if (dstVentilatorUni.z <= 15 && dstVentilatorUni.z > 0)
        {

            AkSoundEngine.SetRTPCValue("VentilatorSound", ventilatorVolume);
        }
        if (dstVentilatorUni.z >= -15 && dstVentilatorUni.z < 0)
        {
            AkSoundEngine.SetRTPCValue("VentilatorSound", ventilatorVolume);
        }
        else
        {
            ventilatorVolume = 0f; 
            AkSoundEngine.SetRTPCValue("VentilatorSound", 0f);
        }
    }
}
