using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterDetector : MonoBehaviour
{
    private Inputs inputs;

    public SphereCollider sphereCollider;
    
    
    private float detection_radius;
    private bool isLetter;
    private List<string> letterList;
    private int indexForLetter;
    public GameMaster gm; 
    
    //public Dictionary<int, string> letterDict = new Dictionary<int, string>();

    // Start is called before the first frame update
   
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

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        inputs.Uni.GetLetter.performed += ctx => GetLetter();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       
        detection_radius = sphereCollider.radius;
        
    }

    private void GetLetter()
    {
        if(isLetter)
        {
            gm.FindLetter();
            DestroyGameObject();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "uni")
        {

            isLetter = true;
        }
        else
        {
            //isLetter = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "uni")
        {
            isLetter = false;
        }
    }

   
    void DestroyGameObject()
    {
        Destroy(gameObject); 
    }
}
