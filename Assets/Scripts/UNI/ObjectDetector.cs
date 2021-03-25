using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{

    private Inputs inputs;

    // List of objects in the object detector
    public List<GameObject> listObj = new List<GameObject>();

    SphereCollider sphere_collider;

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

    // Start is called before the first frame update
    void Start()
    {
        sphere_collider = GetComponent<SphereCollider>();

        inputs.Uni.PressButton.performed += ctx => Interact();
    }

    void Interact()
    {
        if (listObj != null)
        {
            for (int index = 0; index < listObj.Count; index++)
            {
                if ( listObj[index].CompareTag("button"))
                {
                    for (int i = 0; i < listObj[index].GetComponent<button_detector>().caisses.Length; i++)
                    {
                        listObj[index].GetComponent<button_detector>().caisses[i].GetComponent<Rigidbody>().isKinematic = false;
                        listObj[index].GetComponent<button_detector>().caisses[i].GetComponent<Rigidbody>().useGravity = true;
                    }

                    foreach(GameObject light in listObj[index].GetComponent<button_detector>().lights)
                        light.GetComponent<Light>().color = Color.green;
                }

                if (listObj[index].tag == "lever")
                {
                    listObj[index].GetComponent<Lever>().Unlock();
                }

                if (listObj[index].tag == "Button_Platform")
                {
                    listObj[index].GetComponent<ButtonPlatforms>().Increment();
                }
            }
        }

     }

    // Update is called once per frame
    private void OnTriggerEnter(Collider col)
    {


        if (col.CompareTag("blowable") || col.CompareTag("fan") || col.CompareTag("button") || col.CompareTag("lever") || col.CompareTag("Button_Platform"))
        {
            listObj.Add(col.gameObject);
        }

        /*

                if (col.CompareTag("blowable") || col.CompareTag("fan") )
                {
                    FindObjectOfType<Breathing_mechanic>().setBlowObj(col.gameObject);
                }


                if (col.CompareTag("button") || col.CompareTag("lever"))
                {

                    FindObjectOfType<button_detector>().setObj(col.gameObject);
                }*/




    }
    void OnTriggerExit(Collider col)
    {

        if (col.CompareTag("blowable") || col.CompareTag("fan") || col.CompareTag("button") || col.CompareTag("lever") || col.CompareTag("Button_Platform"))
        {
            listObj.Remove(col.gameObject);
        }
/*
        if (col.CompareTag("blowable") || col.CompareTag("fan"))
        {
            FindObjectOfType<Breathing_mechanic>().setBlowObj(null);
        }



        if (col.CompareTag("button") || col.CompareTag("lever"))
        {
            FindObjectOfType<button_detector>().setObj(null);
        }*/

    }
}
