﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_mechanic : MonoBehaviour
{
    private Inputs inputs;

    [HideInInspector]
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
    public bool exhale;

    public KeyCode hold_breath_key;
    public KeyCode exhale_key;

    [SerializeField] private GameObject blowObj;

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

    // Start is called before the first frame update
    void Start()
    {
        breath = max_breath;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputs.Uni.HoldBreath.ReadValue<float>() != 0)
        {
            hold = true;
            current_hold = hold_speed;
        }
        else
        {
            hold = false;
            current_hold = 1;
        }

        if(inputs.Uni.HoldBreath.ReadValue<float>() != 0 && inputs.Uni.Exhale.ReadValue<float>() != 0 && breath >= (max_breath * min_pourc / 100f))
        {
            exhale = true;
            current_exhale = exhale_speed;
            if (blowObj)
            {
                if (blowObj.tag == "blowable")
                    blowObj.GetComponent<ballon>().incAir(1 * Time.deltaTime);
                else if (blowObj.tag == "fan")
                    blowObj.GetComponent<Fan>().incAir(1 * Time.deltaTime);

            }



        }
        else
        {
            exhale = false;
            current_exhale = 1;
        }

        if(!can_breath)
            breath -= current_exhale * breath_speed/current_hold * Time.deltaTime;

        if (breath <= 0) Die();
    }

    public void setBlowObj(GameObject obj)
    {
        blowObj = obj;
    }
    void Die()
    {

    }
}
