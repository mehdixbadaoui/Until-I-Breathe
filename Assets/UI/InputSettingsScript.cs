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

    void Start()
    {
        var map = InputActionAsset.actionMaps;

        foreach(var item in map)
        {
            foreach (var action in item.actions)
            {
                var cell = Instantiate(Cell, Group.transform);

                cell.transform.GetChild(0).GetComponent<Text>().text = action.name;
                cell.transform.GetChild(1).GetComponentInChildren<Text>().text = action.bindings[0].path;           

                cell.name = action.name;
            }
        }
    }

    void Update()
    {
        
    }
}
