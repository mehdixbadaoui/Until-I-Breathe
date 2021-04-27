﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public Text SelectionText;

    public GameMaster gm;
    public GameObject logsLayout;
    public GameObject settingsLayout;
    public GameObject audioLayout;
    public GameObject videoLayout;

    [Serializable]
    public struct Tab
    {
        public GameObject Activable;
        public Selectable Selection;
    }

    public Tab MainTab;
    private Tab currentTab;
    private List<Tab> previousTabs;

    [Serializable]
    public struct PauseMenuButtonData
    {
        public string Name;
        public RectTransform SelectionTransform;

        public Button button;
        public Sprite baseImage;
        public Sprite selectedImage;

        public UnityEvent OnSubmit;

        public Tab Tab;
    }
    public List<PauseMenuButtonData> ButtonsData;

    public UnityEvent Resume;

    private void Awake()
    {
        previousTabs = new List<Tab>();

        currentTab = MainTab;

        previousTabs.Add(currentTab);

        currentTab.Activable.SetActive(true);
    }

    public void Select(int i)
    {
        //SelectionText.text = "< " + ButtonsData[i].Name + " >";
        //Debug.Log("selected");
        //ButtonsData[i].SelectionTransform.gameObject.SetActive(true);
        //ButtonsData[i].button.GetComponent<Image>().sprite = ButtonsData[i].selectedImage;
    }
    public void Deselect(int i)
    {
        //SelectionText.text = "";
        //Debug.Log("unselected");
        //ButtonsData[i].SelectionTransform.gameObject.SetActive(false);
        //ButtonsData[i].button.GetComponent<Image>().sprite = ButtonsData[i].baseImage;


    }

    public void SelectResume()
    {
        gm.GetComponent<GameMaster>().PlayPause();
        settingsLayout.SetActive(false);
        audioLayout.SetActive(false);
        videoLayout.SetActive(false);
        logsLayout.SetActive(false);

    }

    public void SelectLog()
    {
        settingsLayout.SetActive(false);
        audioLayout.SetActive(false);
        videoLayout.SetActive(false);
        logsLayout.SetActive(true);
        //text.SetActive(true);
    }

    public void DeSelectLogs()
    {
        //text.SetActive(false);
    }

    public void SelectSettings()
    {
        logsLayout.SetActive(false);
        settingsLayout.SetActive(true);
        //layout.SetActive(true);
    }

    public void DeSelectSettings()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        //if (!(EventSystem.current.currentSelectedGameObject.name == "Audio Button" || EventSystem.current.currentSelectedGameObject.name == "Video Button"))
        //    //layout.SetActive(false);
        //    Debug.Log("setfalse");
    }

    public void SelectSave(GameObject text)
    {
        audioLayout.SetActive(false);
        videoLayout.SetActive(false);
        settingsLayout.SetActive(false);
        logsLayout.SetActive(false);
        //Code de Ben ici;
        StartCoroutine(SavingText(text));
    }

    public void SelectAudio()
    {
        videoLayout.SetActive(false);
        audioLayout.SetActive(true);
    }

    public void DeSelectAudio()
    {
        //audioLayout.SetActive(false);

    }

    public void SelectVideo()
    {
        audioLayout.SetActive(false);
        videoLayout.SetActive(true);
    }

    IEnumerator SavingText(GameObject text)
    {
        text.SetActive(true);
        float time = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - time < 2f)
        {
            yield return null;
        }
        text.SetActive(false);
        yield return null;
    }

    public void Submit(int i)
    {
        ButtonsData[i].OnSubmit.Invoke();

        if (ButtonsData[i].Tab.Activable)
            OpenTab(i);
    }

    public void OpenTab(int i)
    {
        Deselect(i);

        previousTabs.Add(currentTab);

        currentTab.Activable.SetActive(false);

        currentTab = ButtonsData[i].Tab;

        currentTab.Activable.SetActive(true);
        if (currentTab.Selection)
            currentTab.Selection.Select();
    }
    public void CloseCurrentTab()
    {
        if (previousTabs.Count > 1)
        {
            currentTab.Activable.SetActive(false);

            previousTabs.RemoveAt(previousTabs.Count - 1);

            currentTab = previousTabs[previousTabs.Count - 1];

            currentTab.Activable.SetActive(true);
            if (currentTab.Selection)
                currentTab.Selection.Select();
        }
        else
        {
            TriggerResume();
        }
    }

    public void TriggerResume()
    {
        Resume.Invoke();
    }
}
