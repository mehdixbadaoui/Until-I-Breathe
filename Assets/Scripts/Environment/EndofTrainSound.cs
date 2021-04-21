using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndofTrainSound : MonoBehaviour
{
    private PlayEventSounds playEvent;
    private Vector3 distFromUni;
    // Start is called before the first frame update
    void Start()
    {

        playEvent = GameObject.FindGameObjectWithTag("uni").GetComponent<PlayEventSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        distFromUni = playEvent.CalculateDistanceUniFromObject(this.gameObject.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {

            playEvent.RTPCGameObjectValue(distFromUni, 15, this.gameObject, "Train_klaxon_end_event", "TrainDepartVolume");
        }
    }
}
