using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_detector : MonoBehaviour
{
    private Inputs inputs;

    public GameObject[] caisses;
    public SphereCollider sphereCollider;
    public Rigidbody[] rbCaisses; 

    public KeyCode PushButtonKey;

    private float detection_radius;
    private bool isButtonActive;

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
        sphereCollider = GetComponent<SphereCollider>();
        inputs.Uni.PressButton.performed += ctx => PressButton();

    }

    // Update is called once per frame
    void Update()
    {
        detection_radius = sphereCollider.radius;
        //if (Input.GetKeyDown(PushButtonKey) && isButtonActive)
        //{
        //    Debug.Log("ca touche ici");
        //}
    }

    void PressButton()
    {
        if (isButtonActive)
        {
            for (int i = 0; i < rbCaisses.Length; i++)
            {
                rbCaisses[i].isKinematic = false;
                rbCaisses[i].useGravity = true;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "uni")
        {
            
            isButtonActive = true; 
        }
        else
        {
            //isButtonActive = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "uni")
        {
            isButtonActive = false; 
        }
    }
    
}

