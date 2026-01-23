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
    [SerializeField] public UnityEvent<BlacknessController> OnBlacknessFadedIn;
    [SerializeField] public UnityEvent<BlacknessController> OnBlacknessFadedOut;

    [Header("Debug")]
    [SerializeField] private float TargetAlpha = 0;


    private void Awake() => Instance = this;

    private void Update()
    {
        Color color = BlacknessPannel.color;
        color.a = Mathf.MoveTowards(color.a, TargetAlpha, Speed * Time.deltaTime);
        BlacknessPannel.color = color;

        if (color.a == TargetAlpha)
        {
            if (TargetAlpha == 1f)
                OnBlacknessFadedIn.Invoke(this);
            else
                OnBlacknessFadedOut.Invoke(this);
        }
    }

    public void SetTargetAlpha(float _targetAlpha) => TargetAlpha = _targetAlpha;
}
