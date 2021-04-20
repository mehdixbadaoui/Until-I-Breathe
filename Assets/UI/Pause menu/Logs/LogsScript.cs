using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LogsScript : MonoBehaviour
{
    [Serializable]
    public struct Letter
    {
        public bool Readable;

        public string Title;
        public string Content;
    }

    public List<Letter> Letters;

    public Transform Parent;
    public GameObject LetterTemplateGO;

    public GameObject LetterTransform;
    public Text LetterTitleText;
    public Text LetterContentText;

    private void Awake()
    {
        for (int i = 0; i < Letters.Count; ++i)
        {
            GameObject letterGO = Instantiate(LetterTemplateGO, Parent);
            letterGO.GetComponent<LetterScript>().Initialize(this, Letters[i].Readable, Letters[i].Title, Letters[i].Content);
        }
    }

    public void ReadLetter(LetterScript letter)
    {
        LetterTransform.SetActive(true);
        LetterTitleText.text = letter.Title;
        LetterContentText.text = letter.Content;
    }

    public void QuitLetter()
    {
        LetterTransform.SetActive(false);
    }
}
