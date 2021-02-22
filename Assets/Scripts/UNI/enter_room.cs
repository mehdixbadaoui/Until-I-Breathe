using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    Renderer[] rends;

    Breathing_mechanic breathing_mechanic;

    private void Start()
    {
        if(facade)
            rends = facade.GetComponentsInChildren<Renderer>();

        breathing_mechanic = FindObjectOfType<Breathing_mechanic>();

    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "uni")
        {
            if (facade)
            {
                foreach (Renderer rend  in rends)
                {
                    Vector4 source = rend.material.GetColor("_BaseColor");
                    Vector4 target = new Vector4(source.x, source.y, source.z, 0);
                    if(rend.material.GetColor("_BaseColor").a >= 0)
                        StartCoroutine(fade(rend.material, source, target, .3f));
                }

            }

            breathing_mechanic.breath = breathing_mechanic.max_breath;
            breathing_mechanic.can_breath = true;

        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "uni")
        {
            if (facade)
            {
                foreach (Renderer rend in rends)
                {
                    Vector4 source = rend.material.GetColor("_BaseColor");
                    Vector4 target = new Vector4(source.x, source.y, source.z, 1);

                    StartCoroutine(fade(rend.material, source, target, .3f));

                }
            }
        }

        breathing_mechanic.can_breath = false;
    }



    IEnumerator fade(Material material, Vector4 source, Vector4 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            material.SetColor("_BaseColor", Vector4.Lerp(source, target, (Time.time - startTime) / overTime));
            yield return null;
        }
        material.SetColor("_BaseColor", target);
    }

}
