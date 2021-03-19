using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CancelScript : MonoBehaviour
{
    public KeyCode CancelKeyCode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(CancelKeyCode))
            ExecuteEvents.Execute(gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.cancelHandler);
    }
}
