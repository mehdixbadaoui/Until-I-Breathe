using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballon : MonoBehaviour
{
    public float air;
    public float capacity;
    public float force = 1f;
    public float travel_speed;
    private float travel_speed_val;

    private Vector3 startPosition;

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

    private void Start()
    {
        startPosition = transform.position;
        travel_speed_val = travel_speed;
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


    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("pop"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = startPosition;
            air = 0;
            travel_speed = travel_speed_val;
        }
        else
            travel_speed = 2f;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            Breathing_mechanic bm = other.GetComponent<Breathing_mechanic>();

            Vector3 direction = transform.position - other.transform.position;

            bool can_pfff = (Mathf.Sign(other.transform.TransformDirection(Vector3.forward * other.transform.localScale.z).z) == Mathf.Sign(direction.z));

            if (inputs.Uni.Exhale.ReadValue<float>() != 0 && bm.breath >= (bm.max_breath * bm.min_pourc / 100f) && can_pfff && air >= capacity   )
            {
                transform.Translate(new Vector3(0, 0, direction.z) * force * Time.deltaTime);

            }

        }

    }
}
