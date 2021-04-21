﻿using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string Text;
    public string Person;

    private bool first = true;

    public TMP_Text DialoguBox;
    public TMP_Text DialoguBox_Person;

    public Sprite img1;
    public Sprite img2;


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
            DialoguBox_Person.text = Person;
            //first = false;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0; //alpha == 0 ? 1 : 0;
        DialoguBox.text = Text;

    }
   


}
