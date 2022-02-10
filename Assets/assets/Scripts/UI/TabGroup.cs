using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<GameObject> listSwap;

    public List<TabButton> tabButtons;

    public TabButton selectedTab;

    public Sprite tabHover, tabActive, tabIdle;

    public void SubcribeButton(TabButton _tabButton)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();
        tabButtons.Add(_tabButton);
    }

    public void OntabEnter(TabButton _tabButton)
    {
        ResetTab();
        if (selectedTab == null || _tabButton != selectedTab)
            _tabButton.icon.sprite = tabHover;
    }
    public void OntabExit()
    {
        ResetTab();
    }
    public void OntabSelected(TabButton _tabButton)
    {
        if (selectedTab != null)
            selectedTab.Deselect();
        selectedTab = _tabButton;
        selectedTab.Select();

        ResetTab();

        _tabButton.icon.sprite = tabActive;
        int index = _tabButton.transform.GetSiblingIndex();

        for (int i = 0; i < listSwap.Count; i++)
        {
            if (i == index)
                listSwap[i].SetActive(true);
            else listSwap[i].SetActive(false);
        }
    }
    public void ResetTab()
    {

        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
                continue;
            button.icon.sprite = tabIdle;
        }

    }
}
