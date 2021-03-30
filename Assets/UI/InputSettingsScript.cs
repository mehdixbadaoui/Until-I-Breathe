using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputSettingsScript : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset InputActionAsset;

    public GameObject Group;
    public GameObject Cell;

    private List<InputAction> InputActionList;
    private InputAction _actionToChange;
    private int _bindingIndex;

    void Start()
    {
        var map = InputActionAsset.actionMaps;

        foreach(var item in map)
        {
            foreach (var action in item.actions)
            {
                int bindingIndex = 0;
                foreach(var binding in action.bindings)
                {
                    var cell = Instantiate(Cell, Group.transform);

                    cell.transform.GetChild(0).GetComponent<Text>().text = action.name;
                    cell.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ChangeBinding(action, bindingIndex); });
                    cell.transform.GetChild(1).GetComponentInChildren<Text>().text = binding.path;

                    cell.name = action.name;

                    bindingIndex++;
                }
            }
        }
    }

    private void MakeIAAFromList(InputActionMap[] iamList, Dictionary<InputActionMap, InputAction[]> actionMap)
    {
        InputActionAsset iaa = new InputActionAsset();

        foreach(var iam in iamList)
        {
        }
    }

    private void ChangeBinding(InputAction action, int bindingIndex)
    {
        _actionToChange = action;
        _bindingIndex = bindingIndex;
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        if (_actionToChange != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode != KeyCode.None)
                {
                    Debug.Log((int)e.keyCode);

                }
            }
        }
    }
}
