using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    Renderer[] rends;
    private GameObject uni;
    Breathing_mechanic breathing_mechanic;

    // parameters for ambiant interior  sounds 
    
    private GameObject room_inside;
    
    private BoxCollider room_inside_collider;
    
    private void Start()
    {

        room_inside = this.gameObject;
        room_inside_collider = room_inside.GetComponent<BoxCollider>();
        uni = GameObject.FindGameObjectWithTag("uni");
        
        if (facade)
            rends = facade.GetComponentsInChildren<Renderer>();

    }

    void OnTriggerStay(Collider col)
    {

        if (col.CompareTag("uni"))
        {


            //FADE THE FRONT WALL
            if (facade)
            {
                foreach (Renderer rend in rends)
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

                    StartCoroutine(fade(rend.material, source, target, .3f));

                }
            }
        }
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
