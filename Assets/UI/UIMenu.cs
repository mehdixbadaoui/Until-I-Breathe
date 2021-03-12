using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [Serializable]
    public struct ButtonInfo
    {
        UIButton UIButton;
        Event Event;
    }
    public List<ButtonInfo> buttons;

    public KeyCode NextKeyCode;
    public KeyCode PreviousKeyCode;
    public KeyCode ValidateKeyCode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
