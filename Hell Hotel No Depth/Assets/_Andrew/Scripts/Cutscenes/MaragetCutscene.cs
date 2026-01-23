using UnityEngine;
using UnityEngine.Animations;

public class MaragetCutscene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform MargaretGraphics;
    [SerializeField] private DialogFrameScriptableObject DialogFrame;
    [SerializeField] private Transform TeleportPosition;
    [SerializeField] private TimeWaster _TimeWaster;
    [SerializeField] private ObjectiveScriptableObject Objective;
    [SerializeField] private Animator _Animator;
    [SerializeField] private Animator _DoorAnimator;
    [SerializeField] private Transform IdlePosition;
    [SerializeField] private Transform MovePosition;

    [Header("Settings")]
    [SerializeField] private int CameraQuickFrames = 5;
    [SerializeField] private float WasteTime = 2;
    [SerializeField] private float MoveTime = 3;
    [SerializeField] private float MoveSpeed = 5;

    [Header("Debug")]
    [SerializeField] private InteractableController Source;
    [SerializeField] private Transform TargetPosition;

    private void Start() => TargetPosition = IdlePosition;

    private void Update()
    {
        MargaretGraphics.position = Vector3.MoveTowards(MargaretGraphics.position, TargetPosition.position, MoveSpeed * Time.deltaTime);
    }

    public void OnHovered(InteractableController _) => _Animator.SetFloat("hovered", 1);
    public void OnUnhovered(InteractableController _) => _Animator.SetFloat("hovered", 0);

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
        DialogController.Instance.OnDialogEnded.RemoveListener(OnDialogEnd);
        _TimeWaster.OnTimeWasted.AddListener(OnDoneMoveing);
        _Animator.SetBool("idle", false);
        TargetPosition = MovePosition;
        _DoorAnimator.SetInteger("direction", 90);
        _TimeWaster.WasteTime(MoveTime);
    }

    public void OnDoneMoveing(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(OnDoneMoveing);
        CameraController.Instance.RemoveTarget(MargaretGraphics);
        BlacknessController.Instance.OnBlacknessFadedIn.AddListener(OnBlacknessFadedIn);
        BlacknessController.Instance.SetTargetAlpha(1);
    }

    public void OnBlacknessFadedIn(BlacknessController _)
    {
        BlacknessController.Instance.OnBlacknessFadedIn.RemoveListener(OnBlacknessFadedIn);
        PlayerController.Instance.TeleportTo(TeleportPosition.position);
        CameraController.Instance.Quick(CameraQuickFrames);
        _TimeWaster.OnTimeWasted.AddListener(OnTimeWasted);
        _TimeWaster.WasteTime(WasteTime);
    }

    public void OnTimeWasted(TimeWaster _)
    {
        _TimeWaster.OnTimeWasted.RemoveListener(OnTimeWasted);
        BlacknessController.Instance.OnBlacknessFadedOut.AddListener(OnCutsceneEnd);
        BlacknessController.Instance.SetTargetAlpha(0);
    }

    public void OnCutsceneEnd(BlacknessController _)
    {
        BlacknessController.Instance.OnBlacknessFadedOut.RemoveListener(OnCutsceneEnd);
        DialogController.Instance.OnDialogEnded.RemoveListener(OnDialogEnd);
        PlayerController.Instance.SetPlayerCanMove(true);
        ObjectiveController.Instance.Popup(Objective);

        Source.EndInteraction();
    }
}
