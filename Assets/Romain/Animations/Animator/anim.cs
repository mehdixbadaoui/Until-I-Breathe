using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    Animator myAnimator;
    public float vert;
    void Start () {
        myAnimator = GetComponent<Animator>();
        Debug.Log("MyAniConScript: start => Animator");
    }
	
	// Update is called once per frame
	void Update () {
        
        vert = Input.GetAxis("Horizontal");

        myAnimator.SetFloat("vertical", Mathf.Abs(vert));
        // Debug.Log("vertical = " + Mathf.Abs(Input.GetAxis("Vertical")));
    }
}