using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_mechanic : MonoBehaviour
{
    public float max_breath;
    public float breath;
    public float breath_speed = 1f;

    public float hold_speed = 2f;
    public float current_hold;

    public float exhale_speed = 4;
    public float current_exhale;
    public float min_pourc = 5f;

    public bool can_breath = false;
    public bool hold;

    public KeyCode hold_breath_key;
    public KeyCode exhale_key;


    // Start is called before the first frame update
    void Start()
    {
        breath = max_breath;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(breath);
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

        if(Input.GetKey(hold_breath_key) && Input.GetKey(exhale_key) && breath >= (max_breath * min_pourc / 100f))
        {
            current_exhale = exhale_speed;
        }
        else
        {
            current_exhale = 1;
        }

        if(!can_breath)
            breath -= current_exhale * breath_speed/current_hold * Time.deltaTime;

        if (breath <= 0) Die();
    }

    void Die()
    {

    }
}
