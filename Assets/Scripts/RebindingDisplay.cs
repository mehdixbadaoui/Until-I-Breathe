using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingDisplay : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpAction = null;
    [SerializeField] private Movement playerMovement = null;
    [SerializeField] private TMP_Text bindingDisplayNameText = null;
    [SerializeField] private GameObject startRebindObject = null;
    [SerializeField] private GameObject waitingForInputObject = null;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private const string RebindsKey = "rebinds";

    private void Start()
    {
        string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);

        if (string.IsNullOrEmpty(rebinds)) { return; }
        //playerMovement.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds);
        int bindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);

        bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
    //public void Save()
    //{
    //    string rebinds = playerMovement.PlayerInput.actions.SaveBindingOverridesAsJson();

    //    PlayerPrefs.SetString(RebindsKey, rebinds);
    //}
    public void StartRebinding()
    {
        //string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);

        //if (string.IsNullOrEmpty(rebinds)) { return; }
        //playerMovement.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds);

        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        //playerMovement.PlayerInput.SwitchCurrentActionMap("Menu");


        

        rebindingOperation = jumpAction.action.PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete()).Start() ; 
    }
    private void RebindComplete()
    {
        int bindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);
        bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(jumpAction.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice); 
        rebindingOperation.Dispose();

        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);

        //playerMovement.PlayerInput.SwitchCurrentActionMap("Uni"); 
    }
}
