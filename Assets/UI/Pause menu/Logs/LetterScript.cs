using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LetterScript : MonoBehaviour
{
    private LogsScript logsScript;

    private Button button;
    private Text text;

    public bool Readable = false;

    public string Title = "default title";
    public string Content = "default content";

    public void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();

        button.interactable = Readable;
        text.text = Content;
    }

    public void Initialize(LogsScript ls, bool readable = false, string title = "default title", string content = "default content")
    {
        this.logsScript = ls;

        this.Readable = readable;

        this.Title = title;
        this.Content = content;

        if (button)
            button.interactable = readable;
        if (text)
            text.text = title;
    }

    public void Submit()
    {
        logsScript.ReadLetter(this);
    }
}
