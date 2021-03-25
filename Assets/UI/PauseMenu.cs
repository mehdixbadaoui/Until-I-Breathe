using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text SelectionText;

    public GameObject MainTab;
    private GameObject currentTab;
    private List<GameObject> previousTabs;

    [Serializable]
    public struct PauseMenuButtonData
    {
        public string Name;
        public RectTransform SelectionTransform;
        public UnityEvent Event;
    }
    public List<PauseMenuButtonData> ButtonsData;

    private void Awake()
    {
        previousTabs = new List<GameObject>();

        currentTab = MainTab;
        currentTab.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            CloseCurrentTab();
    }

    public void Select(int i)
    {
        SelectionText.text = "< " + ButtonsData[i].Name + " >";
        ButtonsData[i].SelectionTransform.gameObject.SetActive(true);
    }
    public void Deselect(int i)
    {
        SelectionText.text = "";
        ButtonsData[i].SelectionTransform.gameObject.SetActive(false);
    }
    public void Submit(int i)
    {
        ButtonsData[i].Event.Invoke();
    }

    public void OpenTab(GameObject tab)
    {
        currentTab.SetActive(false);

        previousTabs.Add(currentTab);
        currentTab = tab;

        currentTab.SetActive(true);
    }
    public void CloseCurrentTab()
    {
        if (previousTabs.Count == 0)
            return;
        else
        {
            currentTab.SetActive(false);

            currentTab = previousTabs[previousTabs.Count - 1];
            previousTabs.RemoveAt(previousTabs.Count - 1);

            currentTab.SetActive(true);
        }
    }
}
