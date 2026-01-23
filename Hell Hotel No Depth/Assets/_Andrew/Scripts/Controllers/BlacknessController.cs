using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BlacknessController : MonoBehaviour
{
    [HideInInspector] public static BlacknessController Instance;

    [Header("References")]
    [SerializeField] private Image BlacknessPannel;

    [Header("Settings")]
    [SerializeField] private float Speed = 5;

    [Header("Events")]
    [SerializeField] public UnityEvent<BlacknessController> TargetAlphaReached;

    [Header("Debug")]
    [SerializeField] private float TargetAlpha = 0;
    [SerializeField] private bool JustCalled = false;


    private void Awake() => Instance = this;

    private void Update()
    {
        Color color = BlacknessPannel.color;
        color.a = Mathf.MoveTowards(color.a, TargetAlpha, Speed * Time.deltaTime);
        BlacknessPannel.color = color;

        if (color.a == TargetAlpha && !JustCalled)
        {
            JustCalled = true;
            TargetAlphaReached.Invoke(this);
        }
        else if (color.a != TargetAlpha) JustCalled = false;
    }

    public void SetTargetAlpha(float _targetAlpha) => TargetAlpha = _targetAlpha;
}
