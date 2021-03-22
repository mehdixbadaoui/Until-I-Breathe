using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ChangeVolumeLevel : MonoBehaviour
{
    public Slider thisSlider;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSpecificVolume(string whatValue)
    {
        float sliderValue = thisSlider.value; 
        if(whatValue == "Master")
        {
            masterVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
        }
        if (whatValue == "Music")
        {
            musicVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
        }
        if (whatValue == "Sounds")
        {
            sfxVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("SFXVolume", sfxVolume);
        }
    }
}
