using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnim1 : MonoBehaviour
{
    public Animator anim;
    public TriggerAnim myTr;
    public string animName;
    private GameObject uni;
    private PlayEventSounds playEvent; 

    void Start ()
    {
        anim = GetComponent<Animator>();
        myTr = FindObjectOfType<TriggerAnim>();
        uni = GameObject.FindGameObjectWithTag("uni");
        playEvent = uni.GetComponent<PlayEventSounds>(); 

    }

    void Update ()
    {
        if (myTr.inside == true)
        {   
            anim.Play(animName);
        }
    }
}
