﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundUsine : MonoBehaviour
{

    public GameObject maincamera;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera"); 
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni")
        {
            AkSoundEngine.PostEvent("Usine_sound_break_event", maincamera);
        }
    }
}
