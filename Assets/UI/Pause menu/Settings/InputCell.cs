using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputCell : MonoBehaviour
{
    [SerializeField] private Text ActionName;
    [SerializeField] private Button Button;
    [SerializeField] private Text EffectivePath;

    private InputAction action;
    private InputBinding binding;

    public void Initialize(InputAction action, InputBinding binding)
    {
        this.action = action;
        this.binding = binding;

        ActionName.text = action.name;
        EffectivePath.text = binding.effectivePath;
    }
    public void Rebind()
    {
        action.Disable();
        action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(BindingComplete)
            .WithTargetBinding(action.GetBindingIndex(binding))
            .Start();
    }
    private void BindingComplete(InputActionRebindingExtensions.RebindingOperation obj)
    {
        obj.action.Enable();
        obj.Dispose();

        EffectivePath.text = obj.action.bindings[obj.action.GetBindingIndex(binding)].effectivePath;
    }
}
