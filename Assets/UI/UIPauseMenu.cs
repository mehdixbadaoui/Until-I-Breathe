using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    public Enum Selection {
        get => _selection;
        set
        {
            if(value != _selection)
            {
                UnSelect(_selection);
                _selection = value;
                Select(_selection);
            }
        }
    }
    private Enum _selection;

    public enum Enum { Resume, Logs, Save, Settings, Quit }
    [Serializable]
    public struct ButtonAndEnum
    {
        public Enum Enum;
        public GameObject GO;
    }
    public List<ButtonAndEnum> Buttons;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Select(Enum e)
    {

    }
    private void UnSelect(Enum e)
    {

    }
}
