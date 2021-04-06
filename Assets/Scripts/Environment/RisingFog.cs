using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFog : MonoBehaviour
{
    public CanBreathe SafeZone;
    public float RiseSpeed = 5f;
    public float KillSpeed = 5f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, Mathf.Lerp(0, 1, .5f), 0) * RiseSpeed / 10);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            SafeZone.enabled = false;
            other.GetComponent<Breathing_mechanic>().can_breath = false;
            other.GetComponent<Breathing_mechanic>().breath -= KillSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            SafeZone.enabled = true;
        }

    }
}
