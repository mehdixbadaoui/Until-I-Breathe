using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DeadRobotsShowText : MonoBehaviour
{
    
    private bool first = true;

    public TMP_Text DialoguBox;
    public Canvas canvas;
    private bool isLetter;
    private letterDetector _letterDetector;
    

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        _letterDetector = transform.parent.gameObject.transform.parent.gameObject.GetComponentInChildren<letterDetector>(); 


    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "uni")
        {
            isLetter = _letterDetector.isLetter;
            if (isLetter)
            {
                canvas.gameObject.GetComponent<CanvasGroup>().alpha = 1; //alpha == 0 ? 1 : 0;
                DialoguBox.text = _letterDetector.letter;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "uni")
        {
            canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0; //alpha == 0 ? 1 : 0;
        }
    }
}
