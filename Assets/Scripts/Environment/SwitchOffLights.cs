using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffLights : MonoBehaviour
{
    private Inputs inputs;
    private GameObject[] lights;
    public SphereCollider sphereCollider;
    public KeyCode getSwitchingOffLights = KeyCode.E;

    private bool isSwitchingOffLight;
    // Start is called before the first frame update
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
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("LightOff");
        sphereCollider = GetComponent<SphereCollider>();
        inputs.Uni.SwitchOffLight.performed += ctx => SwitchingOffLights();
    }
    private void FixedUpdate()
    {
        
    }
    private void SwitchingOffLights()
    {
        if(isSwitchingOffLight)
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
