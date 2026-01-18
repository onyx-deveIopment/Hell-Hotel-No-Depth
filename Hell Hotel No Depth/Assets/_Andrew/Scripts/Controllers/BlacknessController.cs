using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BlacknessController : MonoBehaviour
{
    [HideInInspector] public static BlacknessController Instance;

    [Header("References")]
    [SerializeField] private Image BlacknessPannel;

    [Header("Settings")]
    [SerializeField] private float Speed = 5f;

    [Header("Events")]
    [SerializeField] public UnityEvent<BlacknessController> OnBlacknessFadedIn;
    [SerializeField] public UnityEvent<BlacknessController> OnBlacknessFadedOut;

    [Header("Debug")]
    [SerializeField] private bool FadeIn = false;
    [SerializeField] private float TargetAlpha = 0f;


    private void Awake() => Instance = this;

    private void Update()
    {
        TargetAlpha = FadeIn ? 1f : 0f;

        Color color = BlacknessPannel.color;
        color.a = Mathf.MoveTowards(color.a, TargetAlpha, Speed * Time.deltaTime);
        BlacknessPannel.color = color;

        if (color.a == TargetAlpha)
        {
            if (FadeIn)
                OnBlacknessFadedIn.Invoke(this);
            else
                OnBlacknessFadedOut.Invoke(this);
        }
    }

    public void SetFadeIn(bool fadeIn) => FadeIn = fadeIn;
}
