using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public string Text;
    private bool first = true;

    public TMP_Text DialoguBox;
    public Canvas canvas;

    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni") && first)
        {
            //THIS LAGS
            //canvas.gameObject.SetActive(true);
            canvas.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            //DialoguBox.gameObject.SetActive(true);
            DialoguBox.text = Text;
            first = false;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            //canvas.gameObject.SetActive(false);
            canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            DialoguBox.text = "";
        }

    }

}
