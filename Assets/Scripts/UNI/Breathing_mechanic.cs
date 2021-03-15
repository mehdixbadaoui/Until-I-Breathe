using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_mechanic : MonoBehaviour
{
    private Inputs inputs;
    private GameMaster gm;

    [HideInInspector]
    public float max_breath;

    public float breath;
    public float breath_speed = 1f;

    public float hold_speed = 2f;
    public float current_hold;

    public float exhale_speed = 4;
    public float current_exhale;
    public float min_pourc = 5f;

    public bool can_breath = false;
    public bool hold;
    public bool exhale;

    public KeyCode hold_breath_key;
    public KeyCode exhale_key;
    public KeyCode interact;
/*
    [SerializeField] private GameObject blowObj;*/

    //Object detector to get the list of objects
    private ObjectDetector objectDetector;

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
        gm = FindObjectOfType<GameMaster>();
        max_breath = 100f;
        breath = max_breath;

        // Get the object detector
        objectDetector = GetComponentInChildren<ObjectDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(hold_breath_key))
        {
            hold = true;
            current_hold = hold_speed;
        }
        else
        {
            hold = false;
            current_hold = 1;
        }

        if(Input.GetKey(hold_breath_key) && Input.GetKey(exhale_key) && breath >= (max_breath * min_pourc / 100f))
        {
            exhale = true;
            current_exhale = exhale_speed;

            if (objectDetector.listObj !=  null)
            {
                for (int index = 0; index < objectDetector.listObj.Count; index++)
                {
                    if (objectDetector.listObj[index].tag == "blowable")
                    {
                        objectDetector.listObj[index].GetComponent<ballon>().incAir(1 * Time.deltaTime);
                    }
                    if (objectDetector.listObj[index].tag == "fan")
                    {
                        objectDetector.listObj[index].GetComponent<Fan>().incAir(1 * Time.deltaTime);
                    }
                    /*                if (blowObj)
                                    {
                                        if (blowObj.CompareTag("blowable"))
                                            blowObj.GetComponent<ballon>().incAir(1 * Time.deltaTime);
                                        else if (blowObj.CompareTag("fan"))
                                            blowObj.GetComponent<Fan>().incAir(1 * Time.deltaTime);

                                    }*/
                }
            }



        }
        else
        {
            exhale = false;
            current_exhale = 1;
        }

        if(!can_breath)
            breath -= current_exhale * breath_speed/current_hold * Time.deltaTime;

/*        if (Input.GetKeyDown(interact) && blowObj){

            if (blowObj.CompareTag("lever"))
                blowObj.GetComponent<Lever>().door.GetComponent<Door>().locked = false;
        }*/

        if (breath <= 0)
            gm.Die();
    }

/*    public void setBlowObj(GameObject obj)
    {
        blowObj = obj;
    }*/

    void Die()
    {

    }
}
