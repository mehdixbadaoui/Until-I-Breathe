using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class letterDetector : MonoBehaviour
{
    private Inputs inputs;

    public SphereCollider sphereCollider;

    public PlayableDirector Clip;

    private bool didTake = false;

    private float detection_radius;
    public bool isLetter;
    private List<string> letterList;
    private int indexForLetter;
    public GameMaster gm;
    public GameObject pauseMenu;
    private PlayEventSounds playEvent;
    public float maxDistance = 7f;
    

    public string letter;
    public Vector3 dstwithUni; 


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
        sphereCollider = transform.parent.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       
        detection_radius = sphereCollider.radius;
        dstwithUni = playEvent.CalculateDistanceUniFromObject(this.gameObject.transform.position);

    }

    private void GetLetter()
    {
         
        if(isLetter && !didTake)
        {
            didTake = true;
            StartCoroutine(Cinematic(Clip));
            gm.PlayPause();
            pauseMenu.GetComponent<PauseMenu>().logsLayout.SetActive(true);
        }
        
    }
    private IEnumerator Cinematic(PlayableDirector playable)
    {
        Movement.canMove = false;

        if (playable)
        {
            playable.Play();

            yield return new WaitForSeconds((float)playable.duration);

            playable.Stop();
        }

        playEvent.RTPCGameObjectValue(dstwithUni, maxDistance, this.gameObject, "Message_ST2_Branchement_event", "DistWithUniVolume");
        letter = gm.FindLetter();

        Movement.canMove = true;

        DestroyGameObject();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "uni")
        {
            playEvent.RTPCGameObjectValue(dstwithUni, maxDistance, this.gameObject, "ST2_normal_event", "DistWithUniVolume");

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
