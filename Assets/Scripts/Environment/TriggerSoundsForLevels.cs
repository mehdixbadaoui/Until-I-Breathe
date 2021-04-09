using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundsForLevels : MonoBehaviour
{
    private PlayEventSounds playEvent;
    public Vector3 dstTriggerFromUni;
    public float maxDistance = 15f; 
    // Start is called before the first frame update
    void Start()
    {
        playEvent = GameObject.FindGameObjectWithTag("uni").GetComponent<PlayEventSounds>(); 
    }

    // Update is called once per frame
    void Update()
    {
        dstTriggerFromUni = playEvent.CalculateDistanceUniFromObject(this.gameObject.transform.position);
        playEvent.LaunchSoundsonLevels(dstTriggerFromUni, maxDistance, "MusicLevelsVolume");
    }
}
