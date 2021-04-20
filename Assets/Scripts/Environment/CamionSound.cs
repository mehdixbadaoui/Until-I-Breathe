using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamionSound : MonoBehaviour
{
    private PlayEventSounds playEvent;
    private Vector3 distWithUni;
    public float maxDistance = 15f; 
    // Start is called before the first frame update
    void Start()
    {
        playEvent = GameObject.FindGameObjectWithTag("uni").GetComponent<PlayEventSounds>(); 
    }

    // Update is called once per frame
    void Update()
    {
        distWithUni = playEvent.CalculateDistanceUniFromObject(this.gameObject.transform.position);
        playEvent.RTPCGameObjectValue(distWithUni, maxDistance, this.gameObject, "Camion_event", "CamionVolume");
    }
}
