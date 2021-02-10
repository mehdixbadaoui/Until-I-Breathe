using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_mechanic : MonoBehaviour
{
    public float max_breath = 20f;
    public float breath = 20f;
    public float breath_speed = 1f;

    public float hold_speed = 2f;
    public float current_hold;

    public float exhale_speed = 4;
    public float current_exhale;

    public bool can_breath = false;
    public bool hold;

    public KeyCode hold_breath_key;
    public KeyCode exhale_key;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(hold_breath_key))
        {
            hold = true;
            current_hold = hold_speed;
        }
        else
        {
            hold = false;
            current_hold = 1;
        }

        if(Input.GetKey(exhale_key))
        {
            current_exhale = exhale_speed;
        }
        else
        {
            current_exhale = 1;
        }

        if(!can_breath)
            breath -= exhale_speed * breath_speed/current_hold * Time.deltaTime;

        if (breath <= 0) Die();
    }

    void Die()
    {

    }
}
