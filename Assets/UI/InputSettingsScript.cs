using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputSettingsScript : MonoBehaviour
{
    private Inputs inputs;
    private InputActionAsset iaa;

    [SerializeField]
    private Transform _container;
    [SerializeField]
    private InputCell defaultCell;

    private void Awake()
    {
        inputs = new Inputs();

        inputs.Uni.Jump.performed += Action_performed;
        inputs.Uni.Jump.Enable();

        foreach(var iam in inputs.asset.actionMaps)
            foreach(var ia in iam.actions)
                foreach(var ib in ia.bindings)
                {
                    Debug.Log(ib.interactions);
                    var cell = Instantiate(defaultCell, _container);
                    cell.Initialize(ia, ib);
                }
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Action performed");
    }
}
