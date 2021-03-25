using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlAudioVolume : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public void Awake()
    {
        MasterSlider.onValueChanged.AddListener(delegate { AkSoundEngine.SetRTPCValue("MasterVolume", MasterSlider.value); });
        MusicSlider.onValueChanged.AddListener(delegate { AkSoundEngine.SetRTPCValue("MusicVolume", MusicSlider.value); });
        SFXSlider.onValueChanged.AddListener(delegate { AkSoundEngine.SetRTPCValue("SFXVolume", SFXSlider.value); });
    }
}
