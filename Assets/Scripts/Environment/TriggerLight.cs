using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.GlobalIllumination;

public class TriggerLight : MonoBehaviour
{
    public Light directionalLight;
    public float timeToChangeColor;
    public Color newDLightColor;
    public bool isAbleToChangeColorBack;

    Color originalDLightColor;
    float lightChangeTimer;
    bool  changing;

    // Start is called before the first frame update
    void Start()
    {
        originalDLightColor = directionalLight.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (changing)
        {
            lightChangeTimer += Time.deltaTime;
        }
        else
        {
            lightChangeTimer -= Time.deltaTime;
        }
        lightChangeTimer = Mathf.Clamp(lightChangeTimer, 0, timeToChangeColor);
        directionalLight.color = Color.Lerp(originalDLightColor, newDLightColor, lightChangeTimer / timeToChangeColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            changing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni") && isAbleToChangeColorBack)
        {
            changing = false;
        }
    }
}
