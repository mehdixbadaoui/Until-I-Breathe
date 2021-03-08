using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnim : MonoBehaviour
{
    public Animator anim;
    public TriggerAnim myTr;
    public string animName;

    void Start ()
    {
        anim = GetComponent<Animator>();
        myTr = FindObjectOfType<TriggerAnim>();
    }

    void Update ()
    {
        if (myTr.inside == true)
        {
            anim.Play(animName);
        }
    }
}
