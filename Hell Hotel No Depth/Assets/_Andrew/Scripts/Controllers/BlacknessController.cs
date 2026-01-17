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
    [SerializeField] private float Buffer = 0.001f;

    [Header("Events")]
    [SerializeField] public UnityEvent<BlacknessController> OnBlacknessFadedIn;
    [SerializeField] public UnityEvent<BlacknessController> OnBlacknessFadedOut;

    [Header("Debug")]
    [SerializeField] private bool FadeIn = false;
    [SerializeField] private float TargetAlpha = 0;

    private void Awake() => Instance = this;

    public void Update()
    {
        TargetAlpha = FadeIn ? 1 : 0;

        Color color = BlacknessPannel.color;
        color.a = Mathf.Lerp(color.a, TargetAlpha, Speed * Time.deltaTime);
        BlacknessPannel.color = color;

        if (Mathf.Abs(color.a - TargetAlpha) < Buffer)
        {
            if (FadeIn)
                OnBlacknessFadedIn.Invoke(this);
            else
                OnBlacknessFadedOut.Invoke(this);
        }
    }

    public void SetFadeIn(bool _fadeIn) => FadeIn = _fadeIn;
}
