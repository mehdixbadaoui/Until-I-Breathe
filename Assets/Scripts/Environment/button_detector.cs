using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_detector : MonoBehaviour
{
    public GameObject[] caisses;
    public SphereCollider sphereCollider;
    public Rigidbody[] rbCaisses; 

    public KeyCode PushButtonKey;

    private float detection_radius;
    private bool isButtonActive;
    // Start is called before the first frame update
    void Start()
    {
        
        sphereCollider = GetComponent<SphereCollider>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        detection_radius = sphereCollider.radius;
        if (Input.GetKeyDown(PushButtonKey) && isButtonActive)
        {
            for(int i =0; i < rbCaisses.Length; i++)
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

