using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballon : MonoBehaviour
{
    public float air;
    public float capacity;
    public float force = 1f;
    public float travel_speed;

    private Inputs inputs;

    private void Awake()
    {
        inputs = new Inputs();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(air >= capacity)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, travel_speed, 0);
        }

    }

    public void incAir(float amount)
    {
        if(air < capacity)
            air += amount;

    }

    public void Push()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force), ForceMode.Acceleration);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            Breathing_mechanic bm = other.GetComponent<Breathing_mechanic>();

            if (inputs.Uni.Exhale.ReadValue<float>() != 0 && bm.breath >= (bm.max_breath * bm.min_pourc / 100f))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force), ForceMode.Acceleration);
            }

        }

    }
}
