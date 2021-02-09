using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    Material mat;
    public bool visible;

    private void Start()
    {
        visible = true;
        mat = facade.GetComponent<Renderer>().material;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "uni")
        {
            Vector4 source = mat.color;
            Vector4 target = new Vector4(source.x, source.y, source.z, 0/*System.Convert.ToInt32(!visible)*/);

            StartCoroutine(fade(source, target, .3f));

            visible = true;
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
