using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFog : MonoBehaviour
{
    public List<GameObject> SafeZones;
    public float RiseSpeed = 5f;
    public float KillSpeed = 5f;
    // Update is called once per frame

    private void Start()
    {
        SafeZones = new List<GameObject>();
    }
    void Update()
    {
        transform.Translate(new Vector3(0, Mathf.Lerp(0, 1, .5f), 0) * RiseSpeed / 10);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            foreach (GameObject zone in SafeZones)
                zone.GetComponent<CanBreathe>().enabled = false;
            other.GetComponent<Breathing_mechanic>().can_breath = false;
            other.GetComponent<Breathing_mechanic>().breath -= KillSpeed * Time.deltaTime;
        }

        if (other.CompareTag("breathing_zone") && !SafeZones.Contains(other.gameObject))
            SafeZones.Add(other.gameObject);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            foreach (GameObject zone in SafeZones)
                zone.GetComponent<CanBreathe>().enabled = true;
        }
        if (other.CompareTag("breathing_zone") && SafeZones.Contains(other.gameObject))
            SafeZones.Remove(other.gameObject);


    }
}
