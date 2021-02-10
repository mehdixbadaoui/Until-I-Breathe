using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    Material mat;
    public bool visible;

    Breathing_mechanic breathing_mechanic;

    private void Start()
    {
        visible = true;
        mat = facade.GetComponent<Renderer>().material;

        breathing_mechanic = FindObjectOfType<Breathing_mechanic>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "uni")
        {
            Vector4 source = mat.color;
            Vector4 target = new Vector4(source.x, source.y, source.z, 0/*System.Convert.ToInt32(!visible)*/);

            StartCoroutine(fade(source, target, .3f));

            visible = true;

            breathing_mechanic.breath = breathing_mechanic.max_breath;
            breathing_mechanic.can_breath = true;

        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "uni")
        {
            Vector4 source = mat.color;
            Vector4 target = new Vector4(source.x, source.y, source.z, 1/*System.Convert.ToInt32(!visible)*/);

            StartCoroutine(fade(source, target, .3f));

            visible = false;

            breathing_mechanic.can_breath = false;

        }

    }


    IEnumerator fade(Vector4 source, Vector4 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            mat.color = Vector4.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        mat.color = target;
    }

}
