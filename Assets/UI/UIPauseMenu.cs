using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UIPauseMenu : MonoBehaviour
{
    public enum Enum { Resume, Logs, Save, Settings, Quit }
    public UIButton ButtonResume;
    public UIButton ButtonLogs;
    public UIButton ButtonSave;
    public UIButton ButtonSettings;
    public UIButton ButtonQuit;

    public KeyCode NextKeyCode;
    public KeyCode PreviousKeyCode;
    public KeyCode ValidateKeyCode;

    public RectTransform SelectionTransform;

    public UnityEngine.UI.Text Text;

    // Currently selection button
    private UIButton selected;
    private Dictionary<UIButton, UnityAction> dict;

    void Start()
    {
        ChangeSelection(null);

        dict = new Dictionary<UIButton, UnityAction>()
        {
            { ButtonResume, Resume },
            { ButtonLogs, Logs },
            { ButtonSave, Save },
            { ButtonSettings, Settings },
            { ButtonQuit, Quit }
        };

        InitButtonDict(dict);
    }

    // Relative to button management
    private void HighlightButton(UIButton button)
    {
        button.Button.image.color = new Color(1, 0, 0, 1);
    }
    private void LowlightButton(UIButton button)
    {
        button.Button.image.color = new Color(1, 1, 1, 1);
    }
    private void ChangeSelection(UIButton button)
    {
        if(selected)
        {
            selected.Selected = false;
            LowlightButton(selected);
        }

        selected = button;

        if(button)
        {
            HighlightButton(button);
            SelectionTransform.position = button.SelectionTarget.position;
            Text.text = button.Text;
        }
    }
    private void InitButton(UIButton button, UnityAction action)
    {
        button.OnSelection.AddListener(ChangeSelection);
        button.OnValidation.AddListener(action);
    }
    private void InitButtonDict(Dictionary<UIButton, UnityAction> dict)
    {
        foreach(KeyValuePair<UIButton, UnityAction> kvp in dict)
            InitButton(kvp.Key, kvp.Value);
    }
    public void SelectionNavigation(int direction)
    {
        List<UIButton> list = dict.Keys.ToList();
        int index = (list.FindIndex(b => b == selected) + direction) % list.Count;
        if (index < 0)
            index += list.Count;

        UIButton button = list[index];

        button.Activate();
    }

    // Set of methods "au hasard"
    private void Resume()
    {
        Debug.Log("Resume");
    }
    private void Logs()
    {
        Debug.Log("Logs");
    }
    private void Save()
    {
        Debug.Log("Save");
    }
    private void Settings()
    {
        Debug.Log("Settings");
    }
    private void Quit()
    {
        Debug.Log("Quit");
    }

    // le reste
    private void Update()
    {
        if (Input.GetKeyDown(NextKeyCode))
            SelectionNavigation(1);
        if (Input.GetKeyDown(PreviousKeyCode))
            SelectionNavigation(-1);
        if (Input.GetKeyDown(ValidateKeyCode))
            selected.Activate();
    }
}
