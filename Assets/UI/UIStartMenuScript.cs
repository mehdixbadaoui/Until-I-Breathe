using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartMenuScript : MonoBehaviour
{
    public enum StartButtons { Continue, NewGame, Settings, Credits, Quit };
    [Serializable]
    public struct ButtonAndEnum
    {
        public StartButtons Enum;
        public UnityEngine.UI.Button Button;
    }
    public List<ButtonAndEnum> Buttons;

    public Color BaseColor;
    public Color HighlightedColor;
    private GameObject highlightedGO;

    // Start is called before the first frame update
    void Start()
    {
        foreach(ButtonAndEnum bae in Buttons)
        {
            bae.Button.onClick.RemoveAllListeners();

            bae.Button.onClick.AddListener(GetAction(bae.Enum));
            bae.Button.onClick.AddListener(delegate { HighlightSelection(bae.Button.gameObject); });

            bae.Button.gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().color = BaseColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private UnityEngine.Events.UnityAction GetAction(StartButtons e)
    {
        switch(e)
        {
            case StartButtons.Continue:
                return Continue;
            case StartButtons.NewGame:
                return NewGame;
            case StartButtons.Settings:
                return Settings;
            case StartButtons.Credits:
                return Credits;
            case StartButtons.Quit:
                return Quit;
        }

        return null;
    }

    private void HighlightSelection(GameObject go)
    {
        if(highlightedGO)
        {
            UnityEngine.UI.Text previousText = highlightedGO.transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
            previousText.color = BaseColor;
        }

        UnityEngine.UI.Text text = go.transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
        text.color = HighlightedColor;

        highlightedGO = go;
    }

    private void Continue()
    {
        Debug.Log("Continue");
    }
    private void NewGame()
    {
        Debug.Log("NewGame");
    }
    private void Settings()
    {
       
        Debug.Log("Settings");
    }
    private void Credits()
    {
        Debug.Log("Credits");
    }
    private void Quit()
    {
        Debug.Log("Quit");
    }
}
