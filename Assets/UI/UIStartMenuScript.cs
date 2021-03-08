using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartMenuScript : MonoBehaviour
{
    public enum Enum { Continue, NewGame, Settings, Credits, Quit };
    [Serializable]
    public struct ButtonAndEnum
    {
        public Enum Enum;
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

    private UnityEngine.Events.UnityAction GetAction(Enum e)
    {
        switch(e)
        {
            case Enum.Continue:
                return Continue;
            case Enum.NewGame:
                return NewGame;
            case Enum.Settings:
                return Settings;
            case Enum.Credits:
                return Credits;
            case Enum.Quit:
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

    public void Continue()
    {
        Debug.Log("Continue");
    }
    public void NewGame()
    {
        Debug.Log("NewGame");
    }
    public void Settings()
    {
        Debug.Log("Settings");
    }
    public void Credits()
    {
        Debug.Log("Credits");
    }
    public void Quit()
    {
        Debug.Log("Quit");
    }
}
