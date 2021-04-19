using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTrainSound : MonoBehaviour
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
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni") /*&& canstart*/)
        {
            distWithUni = playEvent.CalculateDistanceUniFromObject(gameObject.transform.position);
           
            playEvent.RTPCGameObjectValue(distWithUni, maxDistance, gameObject, "Train_depart_event", "TrainDepartVolume");
            
            

           
        }
    }
}
