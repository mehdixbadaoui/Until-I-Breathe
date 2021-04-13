using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWakeUp : MonoBehaviour
{
    private GameObject uni;

    // Start is called before the first frame update
    void Start()
    {
        // Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebutAnim()
    {
        Movement.canMove = false;
    }
    public void FinAnim()
    {
        Movement.canMove = true;
    }
}
