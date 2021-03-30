using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputSettingsScript : MonoBehaviour
{
    public InputActionReference ActionToRebind;

    private void Awake()
    {
        ActionToRebind.action.performed += Action_performed;
        ActionToRebind.action.Enable();
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Action performed");
    }

    public void Rebind()
    {
        StartRebinding(ActionToRebind);
    }

    public void StartRebinding(InputAction action)
    {
        action.Disable();
        action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(BindingComplete)
            .Start();
    }

    private void BindingComplete(InputActionRebindingExtensions.RebindingOperation obj)
    {
        obj.action.Enable();
        obj.Dispose();
    }

    private void Update()
    {
    }
}
