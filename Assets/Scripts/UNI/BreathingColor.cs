using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingColor : MonoBehaviour
{
    // UNI
    private GameObject uni;

    // Beathing mecanic
    private Breathing_mechanic bm;

    // Start is called before the first frame update
    void Start()
    {

        // Get uni
        uni = GameObject.FindGameObjectWithTag("uni");

        // Get uni
        bm = uni.GetComponent<Breathing_mechanic>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
