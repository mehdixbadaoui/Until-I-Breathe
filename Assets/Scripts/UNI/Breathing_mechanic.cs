using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Breathing_mechanic : MonoBehaviour
{
    private GameMaster gm;

    public Animator myAnimator;

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

    public VisualEffect Vfx;
    private bool breathVfx = false;

    //public KeyCode hold_breath_key;
    //public KeyCode exhale_key;
    //public KeyCode interact;
/*
    [SerializeField] private GameObject blowObj;*/

    //Object detector to get the list of objects
    private ObjectDetector objectDetector;

    private Inputs inputs;

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

        // Get the animator 
        myAnimator = GetComponentInChildren<Animator>();

        // Get the souffle effect
        Vfx = GetComponentInChildren<VisualEffect>() ;
        Vfx.Stop();

        // Get the object detector
        objectDetector = GetComponentInChildren<ObjectDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (inputs.Uni.HoldBreath.ReadValue<float>() != 0)
        //{
        //    //hold = true;
        //    current_hold = hold_speed;
        //}
        //else
        //{
        //    //hold = false;
        //    current_hold = 1;
        //}

        if (inputs.Uni.Exhale.ReadValue<float>() != 0 && breath >= (max_breath * min_pourc / 100f))
        {
            exhale = true;
            current_exhale = exhale_speed;

            if ( breathVfx == false)
            {
                Vfx.Play();
                breathVfx = true;
            }

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
                        myAnimator.Play("BreathingFan", 1);
                        myAnimator.Play("BreathingFan", 2);

                        //TODO MEttre un canMove = false

                        
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

            if (breathVfx == true)
            {
                Vfx.Stop();
                breathVfx = false;
            }
        }

        if(!can_breath)
            breath -= current_exhale * breath_speed/ hold_speed * Time.deltaTime;

        /*        if (Input.GetKeyDown(interact) && blowObj){

                    if (blowObj.CompareTag("lever"))
                        blowObj.GetComponent<Lever>().door.GetComponent<Door>().locked = false;
                }*/

        if (breath <= 0)
        {
            //yield WaitForAnimation(animation );
            gm.Die();
        }
    }

/*    public void setBlowObj(GameObject obj)
    {
        blowObj = obj;
    }*/

}
