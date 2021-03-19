using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDialogScript : MonoBehaviour
{
    /// <summary>
    /// Text to use for dialogs
    /// </summary>
    public Text Text;

    /// <summary>
    /// Image to use for dialogs
    /// </summary>
    public Image Image;

    /// <summary>
    /// List of dialog strings
    /// </summary>
    public List<string> DialogStringList;

    private int _dialogIndex;

    /// <summary>
    /// Event triggered when the dialogs have ended
    /// </summary>
    public UnityEvent ReachedEndOfDialog;
    private int dialogIndex
    {
        get
        {
            return _dialogIndex;
        }
        set
        {
            if(value < DialogStringList.Count)
            {
                _dialogIndex = value;

                Text.text = DialogStringList[_dialogIndex];
            }
            else
            {
                ReachedEndOfDialog.Invoke();
            }
        }
    }

    /// <summary>
    /// Key used to iterate inside the dialog
    /// </summary>
    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        dialogIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            Advance();
        }
    }

    /// <summary>
    /// Changes the dialog and resets the dialog index (go back to the beginning).
    /// </summary>
    /// <param name="dsl"> The new list of dialog' strings </param>
    public void SetDialogStringList(List<string> dsl)
    {
        DialogStringList = dsl;
        dialogIndex = 0;
    }

    /// <summary>
    /// Advance in the dialog
    /// </summary>
    public void Advance()
    {
        dialogIndex++;
    }
}
