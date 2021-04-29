using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginVolantVolume : MonoBehaviour
{
    private PlayEventSounds playEvent;
    private bool firstEntry = false;
    public float volumeAnnonce = 50f;
    // Start is called before the first frame update

    void Start()
    {
        playEvent = GameObject.FindGameObjectWithTag("uni").GetComponent<PlayEventSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        AkSoundEngine.SetRTPCValue("EnginVolantVolume", volumeAnnonce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni")
        {
            if (firstEntry == false)
            {
                playEvent.PlayEventWithoutRTPC("EnginVolant_event", this.gameObject);
                firstEntry = true;
            }
        }
    }
}
