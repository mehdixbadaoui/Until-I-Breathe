using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    private Image image;
    Breathing_mechanic bm;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        bm = FindObjectOfType<Breathing_mechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = bm.breath / bm.max_breath;
    }
}
