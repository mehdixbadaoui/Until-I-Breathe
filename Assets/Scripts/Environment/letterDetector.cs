using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterDetector : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public KeyCode GetLetterKey;
    
    private float detection_radius;
    private bool isLetter;
    private List<string> letterList;
    private int indexForLetter;
    public GameMaster gm; 
    
    //public Dictionary<int, string> letterDict = new Dictionary<int, string>();

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        sphereCollider = GetComponent<SphereCollider>();
    }
    

    // Update is called once per frame
    void Update()
    {
       
        detection_radius = sphereCollider.radius;
        if (Input.GetKeyDown(GetLetterKey) && isLetter)
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
