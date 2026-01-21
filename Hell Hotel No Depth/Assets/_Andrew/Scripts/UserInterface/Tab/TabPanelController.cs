using UnityEngine;

public class TabPanelController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string Name = "temp";

    [Header("Debug")]
    [SerializeField] private TabController _TabController;

    public string GetName() => Name;
    public void SetTabController(TabController _tabController) => _TabController = _tabController;
}
