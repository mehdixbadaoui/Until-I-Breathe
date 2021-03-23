using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public UnityEvent Resume;
    public UnityEvent Quit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerResume()
    {
        Debug.Log("Invoking resume events");

        Resume.Invoke();
    }

    public void TriggerQuit()
    {
        Debug.Log("Invoking quit events");

        Quit.Invoke();
    }
}
