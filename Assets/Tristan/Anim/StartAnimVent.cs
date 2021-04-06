using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimVent : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Fan>().isOn == true)
        {
            animController.Play("AnimVentTuto");
        }
    }
}
