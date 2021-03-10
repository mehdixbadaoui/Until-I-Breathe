using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stand2crouch : MonoBehaviour {
 
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
        // inputs.Uni.Crouch.performed += ctx => Crouch();

 }
 
 // Update is called once per frame
 void Update () {
    //     if (inputs.Uni.Crouch.triggered && inputs.Uni.Crouch.ReadValue<float>() != 0)
    //     {
    //         anim.Play("stand2crouch");
    //     }
    //    else if (inputs.Uni.Crouch.triggered && inputs.Uni.Crouch.ReadValue<float>() == default)
    //     {
    //         anim.Play("crouched2stand");
    //     }
    }

    // void Crouch(){
    //     anim.Play("stand2crouch");

    // }
}