using UnityEngine;

public class CoffeeCutscene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimeWaster _TimeWaster;
    [SerializeField] private ObjectiveScriptableObject Objective;
    [SerializeField] private Transform TeleportPosition;

    [Header("Settings")]
    [SerializeField] private float FirstFadeAmount = 0.7f;
    [SerializeField] private float SecondFadeAmount = 0.2f;
    [SerializeField] private float ThirdFadeAmount = 0.9f;
    [SerializeField] private float FourthFadeAmount = 0.4f;
    [SerializeField] private float FadeSpacing = 0.5f;
    [Space]
    [SerializeField] private float PassedOutTime = 5;
    
    [Header("Debug")]
    [SerializeField] private InteractableController Source;

    // CUTSCENE OUTLINE
    // Interact
    // Start to feel weird and dizzy.
    // Fade in and out a few times.
    // Fade to black.
    // Wait time.
    // Fade back in.
    // "I don't like this sketch place".
    // Objective "find a way out".
    // End cutscene, allow player to move.

    public void OnInteract(InteractableController _interactableController)
    {
        Source = _interactableController;
        PlayerController.Instance.SetPlayerCanMove(false);

        StartFirstFade();
    }

    public void StartFirstFade()
    {
        BlacknessController.Instance.TargetAlphaReached.AddListener(EndOfFirstFade);
        BlacknessController.Instance.SetTargetAlpha(FirstFadeAmount);
    }

    public void EndOfFirstFade(BlacknessController _)
    {
        BlacknessController.Instance.TargetAlphaReached.RemoveListener(EndOfFirstFade);
        _TimeWaster.OnTimeWasted.AddListener(StartSecondFade);
        _TimeWaster.WasteTime(0.5f);
    }

    public void StartSecondFade(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(StartSecondFade);
        BlacknessController.Instance.TargetAlphaReached.AddListener(EndOfSecondFade);
        BlacknessController.Instance.SetTargetAlpha(SecondFadeAmount);
    }

    public void EndOfSecondFade(BlacknessController _)
    {
        BlacknessController.Instance.TargetAlphaReached.RemoveListener(EndOfSecondFade);
        _TimeWaster.OnTimeWasted.AddListener(StartThirdFade);
        _TimeWaster.WasteTime(FadeSpacing);
    }

    public void StartThirdFade(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(StartThirdFade);
        BlacknessController.Instance.TargetAlphaReached.AddListener(EndOfThirdFade);
        BlacknessController.Instance.SetTargetAlpha(ThirdFadeAmount);
    }

    public void EndOfThirdFade(BlacknessController _)
    {
        BlacknessController.Instance.TargetAlphaReached.RemoveListener(EndOfThirdFade);
        _TimeWaster.OnTimeWasted.AddListener(StartFourthFade);
        _TimeWaster.WasteTime(FadeSpacing);
    }

    public void StartFourthFade(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(StartFourthFade);
        BlacknessController.Instance.TargetAlphaReached.AddListener(EndOfFourthFade);
        BlacknessController.Instance.SetTargetAlpha(FourthFadeAmount);
    }

    public void EndOfFourthFade(BlacknessController _)
    {
        BlacknessController.Instance.TargetAlphaReached.RemoveListener(EndOfFourthFade);
        _TimeWaster.OnTimeWasted.AddListener(StartPassOut);
        _TimeWaster.WasteTime(FadeSpacing);
    }

    public void StartPassOut(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(StartPassOut);
        BlacknessController.Instance.SetTargetAlpha(1);
        _TimeWaster.OnTimeWasted.AddListener(EndPassOut);
        _TimeWaster.WasteTime(PassedOutTime);
    }

    public void EndPassOut(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(EndPassOut);
        PlayerController.Instance.TeleportTo(TeleportPosition.position);
        CameraController.Instance.Quick(5);
        BlacknessController.Instance.TargetAlphaReached.AddListener(EndCutscene);
        BlacknessController.Instance.SetTargetAlpha(0);
    }

    public void EndCutscene(BlacknessController _)
    {
        BlacknessController.Instance.TargetAlphaReached.RemoveListener(EndCutscene);
        PlayerController.Instance.SetPlayerCanMove(true);
        ObjectiveController.Instance.Popup(Objective);

        Source.EndInteraction();
    }
}
