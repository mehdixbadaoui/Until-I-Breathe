using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public string Text;
    private bool first = true;

    public TMP_Text DialoguBox;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni") && first)
        {
            DialoguBox.text = Text;
            first = false;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            DialoguBox.text = "";
        }

    }

}
