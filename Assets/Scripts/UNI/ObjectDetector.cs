using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{

    private Inputs inputs;

    // List of objects in the object detector
    public List<GameObject> listObj = new List<GameObject>();

    SphereCollider sphere_collider;
    private GameObject uni; 

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
        uni = GameObject.FindGameObjectWithTag("uni");
        inputs.Uni.PressButton.performed += ctx => Interact();
    }

    void Interact()
    {
        if (listObj != null)
        {
            for (int index = 0; index < listObj.Count; index++)
            {
                AkSoundEngine.PostEvent("Levier_event", uni);
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

                else if (listObj[index].CompareTag("lever"))
                {
                    listObj[index].GetComponent<Lever>().Unlock();

                }

                else if (listObj[index].GetComponent<LeverEnceinte>())
                {
                    listObj[index].GetComponent<LeverEnceinte>().isOn = true;

                }

                else if (listObj[index].CompareTag("RisingFogLever"))
                {
                    listObj[index].GetComponent<RisingFogLever>().LaunchGaz();
                    
                }

                else if (listObj[index].CompareTag("Button_Platform"))
                {
                    listObj[index].GetComponent<ButtonPlatforms>().Increment();
                }

                else if (listObj[index].CompareTag("Lever_Platform"))
                {
                    listObj[index].GetComponent<LeverPlatforms>().GoTo();
                    
                }
            }
        }
     }

    // Update is called once per frame
    private void OnTriggerEnter(Collider col)
    {


        if (col.CompareTag("blowable") || col.CompareTag("fan")
            || col.CompareTag("button") || col.CompareTag("lever")
            || col.CompareTag("Button_Platform") || col.CompareTag("Lever_Platform")
            || col.CompareTag("RisingFogLever") || col.GetComponent<LeverEnceinte>())
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

        if (col.CompareTag("blowable") || col.CompareTag("fan") || col.CompareTag("button") || col.CompareTag("lever") || col.CompareTag("Button_Platform") || col.CompareTag("Lever_Platform"))
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
