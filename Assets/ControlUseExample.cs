using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Inputs;

public class ControlUseExample : MonoBehaviour, IUniActions
{
    [SerializeField]
    private InputActionAsset iaa;

    public Inputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new Inputs();

        inputs.Uni.SetCallbacks(this);

        inputs.Uni.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        if(inputs != null)
            inputs.Uni.Enable();
    }

    void OnDisable()
    {
        inputs.Uni.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping " + context.phase);
    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnClimb_Up(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnLet_Go(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnDetach(InputAction.CallbackContext context)
    {
        Debug.Log("Detaching " + context.phase);
    }

    public void OnGrapple_Vert(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnHoldBreath(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnExhale(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnNextHook(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPrevHook(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnDie(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPressButton(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnMove_Box(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
