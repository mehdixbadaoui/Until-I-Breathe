using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffLights : MonoBehaviour
{
    private GameObject[] lights;
    public SphereCollider sphereCollider;
    public KeyCode getSwitchingOffLights = KeyCode.E;

    private bool isSwitchingOffLight;
    // Start is called before the first frame update
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("LightOff");
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(getSwitchingOffLights) && isSwitchingOffLight)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false); 
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "uni")
        {

            isSwitchingOffLight = true;
        }
        else
        {
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "uni")
        {
            isSwitchingOffLight = false;
        }
    }


}
