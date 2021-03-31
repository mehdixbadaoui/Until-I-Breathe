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
        canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni") /*&& first*/)
        {
            //float alpha = canvas.gameObject.GetComponent<CanvasGroup>().alpha;

            //THIS LAGS
            //canvas.gameObject.SetActive(true);

            canvas.gameObject.GetComponent<CanvasGroup>().alpha = 1; //alpha == 0 ? 1 : 0;
            DialoguBox.text = Text;
            //first = false;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0; //alpha == 0 ? 1 : 0;
        DialoguBox.text = Text;

    }

}
