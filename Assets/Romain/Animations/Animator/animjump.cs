using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animjump : MonoBehaviour {
 
public Animator anim;

private Inputs inputs;

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

 // Use this for initialization
 void Start () {
    anim = GetComponent<Animator>();
    inputs.Uni.Jump.performed += ctx => Jump();
 }
 
 // Update is called once per frame
 void Update () {
        // if (Input.GetKeyDown("space"))
        // {
        //     anim.Play("idlejump");
        // }
    }

    void Jump(){
        anim.Play("idlejump");
    }
}