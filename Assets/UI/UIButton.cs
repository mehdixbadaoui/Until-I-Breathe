using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class UIButton : MonoBehaviour
{
    public UnityEngine.UI.Button Button { get; private set; }
    public RectTransform SelectionTarget;
    public string Text;

    public UnityEvent<UIButton> OnSelection;

    private bool _selected;
    public bool Selected
    {
        get => _selected;
        set
        {
            if(_selected != value)
            {
                _selected = value;

                if (value)
                    OnSelection.Invoke(this);
            }
        }
    }

    public UnityEvent OnValidation;

    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<UnityEngine.UI.Button>();

        Button.onClick.AddListener(Activate);
    }

    public void Activate()
    {
        if(_selected)
        {
            OnValidation.Invoke();
        }
        else
        {
            Selected = true;
        }
    }

    public void Click()
    {
        ExecuteEvents.Execute(gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }
}
