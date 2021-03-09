using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stand2crouch : MonoBehaviour {
 
public Animator anim;

 // Use this for initialization
 void Start () {
        anim = GetComponent<Animator>();
 }
 
 // Update is called once per frame
 void Update () {
        if (Input.GetKeyDown("s"))
        {
            anim.Play("stand2crouch");
        }
       else if (Input.GetKeyUp("s"))
        {
            anim.Play("crouched2stand");
        }
    }
}