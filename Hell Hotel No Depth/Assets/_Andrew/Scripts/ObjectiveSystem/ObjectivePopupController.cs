using UnityEngine;
using UnityEngine.UI;

public class ObjectivePopupController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _Text;
    [SerializeField] private TimeWaster _TimeWaster;
    [SerializeField] private RectTransform Container;

    [Header("Settings")]
    [SerializeField] private float DisplayTime = 3;
    [SerializeField] private float Height = 400;
    [SerializeField] private float LerpSpeed = 10;

    [Header("Debug")]
    [SerializeField] private Vector2 StartPosition;
    [SerializeField] private Vector2 TargetPosition;

    public void Update()
    {
        Container.anchoredPosition = Vector2.Lerp(Container.anchoredPosition, TargetPosition, Time.deltaTime * LerpSpeed);
    }

    public void StartObjectivePopup(ObjectiveScriptableObject _objective)
    {
        _Text.text = _objective.Text;

        StartPosition = Container.anchoredPosition;
        TargetPosition = StartPosition; 
        Container.anchoredPosition = StartPosition - new Vector2(0, Height);

        _TimeWaster.OnTimeWasted.AddListener(OnAtTop);
        _TimeWaster.WasteTime(DisplayTime);
    }

    private void OnAtTop(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(OnAtTop);
        TargetPosition = StartPosition - new Vector2(0, Height);
        _TimeWaster.OnTimeWasted.AddListener(OnOffScreen);
        _TimeWaster.WasteTime(DisplayTime);
    }

    private void OnOffScreen(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(OnOffScreen);
        Destroy(gameObject);
    }
}
