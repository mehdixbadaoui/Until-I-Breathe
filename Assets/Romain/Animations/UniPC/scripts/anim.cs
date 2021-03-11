 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    private Inputs inputs;

    Animator myAnimator;
    public float vert;

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

    void Start () {
        myAnimator = GetComponent<Animator>();
        Debug.Log("MyAniConScript: start => Animator");
    }
	
	// Update is called once per frame
	void Update () {
        
        vert = Input.GetAxis("Horizontal");
        // vert = inputs.Uni.Walk.ReadValue<float>();:

        myAnimator.SetFloat("vertical", Mathf.Abs(vert));
        // Debug.Log("vertical = " + Mathf.Abs(Input.GetAxis("Vertical")));
    }
}