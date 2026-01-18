using UnityEngine;

public class MaragetCutscene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform MargaretGraphics;
    [SerializeField] private DialogFrameScriptableObject DialogFrame;
    [SerializeField] private Transform TeleportPosition;

    [Header("Settings")]
    [SerializeField] private int CameraQuickFrames = 5;
    [SerializeField] private float WasteTime = 2f;

    [Header("Debug")]
    [SerializeField] private InteractableController Source;

    public void OnInteract(InteractableController _source)
    {
        Source = _source;

        PlayerController.Instance.SetPlayerCanMove(false);
        CameraController.Instance.AddTarget(MargaretGraphics);
        DialogController.Instance.LoadFrame(DialogFrame);
        DialogController.Instance.OnDialogEnded.AddListener(OnDialogEnd);
    }

    public void OnDialogEnd(DialogController _)
    {
        BlacknessController.Instance.OnBlacknessFadedIn.AddListener(OnBlacknessFadedIn);
        BlacknessController.Instance.SetFadeIn(true);
    }

    public void OnBlacknessFadedIn(BlacknessController _)
    {
        BlacknessController.Instance.OnBlacknessFadedIn.RemoveListener(OnBlacknessFadedIn);
        PlayerController.Instance.TeleportTo(TeleportPosition.position);
        CameraController.Instance.RemoveTarget(MargaretGraphics);
        CameraController.Instance.Quick(CameraQuickFrames);
        TimeWaster.Instance.OnTimeWasted.AddListener(OnTimeWasted);
        TimeWaster.Instance.WasteTime(WasteTime);
    }

    public void OnTimeWasted(TimeWaster _)
    {
        TimeWaster.Instance.OnTimeWasted.RemoveListener(OnTimeWasted);
        BlacknessController.Instance.OnBlacknessFadedOut.AddListener(OnCutsceneEnd);
        BlacknessController.Instance.SetFadeIn(false);
    }

    public void OnCutsceneEnd(BlacknessController _)
    {
        BlacknessController.Instance.OnBlacknessFadedOut.RemoveListener(OnCutsceneEnd);
        DialogController.Instance.OnDialogEnded.RemoveListener(OnDialogEnd);
        PlayerController.Instance.SetPlayerCanMove(true);

        Source.EndInteraction();
    }
}
