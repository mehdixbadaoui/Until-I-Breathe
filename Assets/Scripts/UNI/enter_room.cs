using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    Renderer[] rends;

    private void Start()
    {
        if(facade)
            rends = facade.GetComponentsInChildren<Renderer>();
    }
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("uni"))
        {
            //FADE THE FRONT WALL
            if (facade)
            {
                foreach (Renderer rend  in rends)
                {
                    Vector4 source = rend.material.GetColor("_BaseColor");
                    Vector4 target = new Vector4(source.x, source.y, source.z, 0);
                    if (rend.material.GetColor("_BaseColor").a != 0)
                    {
                        //StartCoroutine(fade(rend.material, source, target, .3f));
                        rend.material.SetColor("_BaseColor", Vector4.Lerp(rend.material.GetColor("_BaseColor"), target, .3f));
                    }
                }

            }


        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("uni"))
        {
            if (facade)
            {
                foreach (Renderer rend in rends)
                {
                    Vector4 source = rend.material.GetColor("_BaseColor");
                    Vector4 target = new Vector4(source.x, source.y, source.z, 1f);

                    StartCoroutine(Fade(rend.material, source, target, .3f));

                }
            }
        }

    }



    IEnumerator Fade(Material material, Vector4 source, Vector4 target, float overTime)
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
