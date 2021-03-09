using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animjump : MonoBehaviour {
 
public Animator anim;

 // Use this for initialization
 void Start () {
        anim = GetComponent<Animator>();
 }
 
 // Update is called once per frame
 void Update () {
        if (Input.GetKeyDown("space"))
        {
            anim.Play("idlejump");
        }
    }
}