using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteNacelleRicheClose : MonoBehaviour
{

    public Animator anim;
    public TriggerAnim myTr;
    private GameObject uni;
    private PlayEventSounds playEvent; 
    public string animName;

    // Start is called before the first frame update
    void Start ()
    {
        anim = GetComponent<Animator>();
        myTr = FindObjectOfType<TriggerAnim>();
        uni = GameObject.FindGameObjectWithTag("uni");
        playEvent = uni.GetComponent<PlayEventSounds>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (myTr.inside == true)
         anim.Play(animName);
    }
}
