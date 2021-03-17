using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : MonoBehaviour
{
    public float max_capacity = 200;
    public float initial_fil = 0;
    public float current_fill = 0;
    public float fill_speed = 0f;

    public GameObject Object;
    public GameObject Light;

    Color UnlockedColor;

    Breathing_mechanic bm;
    // Start is called before the first frame update
    void Start()
    {
        bm = FindObjectOfType<Breathing_mechanic>();
        UnlockedColor = Light.GetComponent<Light>().color;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "uni")
        {
            if (bm.exhale && current_fill < max_capacity)
            {
                current_fill += fill_speed * Time.deltaTime;
                if(UnlockedColor.r > .005f && UnlockedColor.g < .095)
                    UnlockedColor = new Color(.5f - .5f * current_fill / max_capacity, .5f +  .5f * current_fill / max_capacity, UnlockedColor.b);
                Light.GetComponent<Light>().color = UnlockedColor;
            }


            if(current_fill >= max_capacity && Object)
            {
                Object.GetComponent<Rigidbody>().useGravity = true;
            }

        }

    }
}
