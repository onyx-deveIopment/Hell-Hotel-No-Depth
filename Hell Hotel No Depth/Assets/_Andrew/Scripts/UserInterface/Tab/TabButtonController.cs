using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButtonController : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text Positions")]
    [SerializeField] private RectTransform DefaultTextPosition;
    [SerializeField] private RectTransform SelectedTextPosition;

    [Header("References")]
    [SerializeField] private Button Button;
    [SerializeField] private Image Image;
    [SerializeField] private Text Text;

    [Header("Graphics")]
    [SerializeField] private Sprite DefaultGraphic;
    [SerializeField] private Sprite HoveredGraphic;
    [SerializeField] private Sprite SelectedGraphic;

    private TabController tabController;
    private int index;
    private bool isHovered;

    private RectTransform textRect;

    private void Awake()
    {
        textRect = Text.GetComponent<RectTransform>();
    }

    public void SetTabController(TabController controller)
    {
        tabController = controller;
        Button.onClick.AddListener(OnClick);
    }

    public void SetName(string name) => Text.text = name;
    public void SetIndex(int i) => index = i;

    public void OnClick()
    {
        tabController.SelectTab(index);
    }

    public void RefreshVisual()
    {
        bool isSelected = tabController.GetSelectedTabIndex() == index;

        if (isSelected) Image.sprite = SelectedGraphic;
        else if (isHovered) Image.sprite = HoveredGraphic;
        else Image.sprite = DefaultGraphic;

        ApplyTextLayout(isSelected ? SelectedTextPosition : DefaultTextPosition);
    }

    private void ApplyTextLayout(RectTransform source)
    {
        if (source == null || textRect == null)
            return;

        textRect.anchorMin = source.anchorMin;
        textRect.anchorMax = source.anchorMax;
        textRect.pivot = source.pivot;
        textRect.anchoredPosition = source.anchoredPosition;
        textRect.sizeDelta = source.sizeDelta;
        textRect.localRotation = source.localRotation;
        textRect.localScale = source.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        RefreshVisual();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        RefreshVisual();
    }
}