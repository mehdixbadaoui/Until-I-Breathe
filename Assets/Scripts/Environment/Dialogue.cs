using TMPro;
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

    //ST2_Sound
    public bool doOnce;
    public bool ST2_Voice;
    public bool ST2_sad;
    public bool ST2_normal;
    public bool ST2_danger;
    private bool doEveryTime;
    private PlayEventSounds playEventSounds; 


    private void Start()
    {
        doEveryTime = !doOnce;
        playEventSounds = GameObject.FindGameObjectWithTag("uni").GetComponent<PlayEventSounds>();
        canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("uni"))
        {
            if(doOnce)
            {
                if(ST2_Voice)
                {
                    if (ST2_normal)
                    {
                        playEventSounds.PlayEventWithoutRTPC("ST2_normal_event", this.gameObject);

                    }
                    else if (ST2_sad)
                    {
                        playEventSounds.PlayEventWithoutRTPC("ST2_triste_event", this.gameObject);
                    }
                    else if (ST2_danger)
                    {
                        playEventSounds.PlayEventWithoutRTPC("ST2_danger_event", this.gameObject);
                    }
                }
                doOnce = false; 
            }
            else if(doEveryTime)
            {
                if (ST2_Voice)
                {
                    if (ST2_normal)
                    {
                        playEventSounds.PlayEventWithoutRTPC("ST2_normal_event", this.gameObject);

                    }
                    else if (ST2_sad)
                    {
                        playEventSounds.PlayEventWithoutRTPC("ST2_triste_event", this.gameObject);
                    }
                    else if (ST2_danger)
                    {
                        playEventSounds.PlayEventWithoutRTPC("ST2_danger_event", this.gameObject);
                    }
                }
            }
        }
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
