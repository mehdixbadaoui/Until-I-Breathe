using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterDetector : MonoBehaviour
{
    private Inputs inputs;

    public SphereCollider sphereCollider;
    
    
    private float detection_radius;
    public bool isLetter;
    private List<string> letterList;
    private int indexForLetter;
    public GameMaster gm;
    private PlayEventSounds playEvent;
    public float maxDistance = 7f;
    private Vector3 dstwithUni;
    public Vector3 offsetDeadRobotRotation;
    public Vector3 offsetDeadRobotTranslation;
    public string letter;
    

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
        playEvent = GameObject.FindGameObjectWithTag("uni").GetComponent<PlayEventSounds>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        inputs.Uni.GetLetter.performed += ctx => GetLetter();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       
        detection_radius = sphereCollider.radius;
        dstwithUni = playEvent.CalculateDistanceUniFromObject(this.gameObject.transform.position);

    }

    private void GetLetter()
    {
         
        if(isLetter)
        {
            //playEvent.RTPCGameObjectValue(dstwithUni, maxDistance, this.gameObject, "Message_ST2_Branchement_event", "DistWithUniVolume");
            letter = gm.FindLetter();

            //DestroyGameObject();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "uni")
        {

            isLetter = true;
            //playEvent.RTPCGameObjectValue(dstwithUni, maxDistance, GameObject.FindGameObjectWithTag("ST2"), "Message_ST2_decrypte_event", "DistWithUniVolume");
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
