using UnityEngine;
using System.Collections.Generic;

public class TabController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject TabButtonPrefab;
    [SerializeField] private RectTransform TabButtonParent;
    [SerializeField] private RectTransform TabPanelParent;

    [Header("Debug")]
    [SerializeField] private List<TabPanelController> TabPanels = new();
    [SerializeField] private List<TabButtonController> TabButtons = new();
    [SerializeField] private int SelectedTabIndex = 0;

    private void Start()
    {
        TabPanels.Clear();
        TabButtons.Clear();

        int index = 0;

        foreach (RectTransform panel in TabPanelParent)
        {
            TabPanelController tabPanel = panel.GetComponent<TabPanelController>();
            TabPanels.Add(tabPanel);
            tabPanel.SetTabController(this);

            TabButtonController tabButton =
                Instantiate(TabButtonPrefab, TabButtonParent)
                .GetComponent<TabButtonController>();

            tabButton.SetTabController(this);
            tabButton.SetName(tabPanel.GetName());
            tabButton.SetIndex(index);

            TabButtons.Add(tabButton);

            index++;
        }

        SelectTab(SelectedTabIndex);
    }

    public void SelectTab(int index)
    {
        SelectedTabIndex = index;

        for (int i = 0; i < TabPanels.Count; i++)
        {
            TabPanels[i].gameObject.SetActive(i == SelectedTabIndex);
            TabButtons[i].RefreshVisual();
        }
    }

    public int GetSelectedTabIndex() => SelectedTabIndex;
}
