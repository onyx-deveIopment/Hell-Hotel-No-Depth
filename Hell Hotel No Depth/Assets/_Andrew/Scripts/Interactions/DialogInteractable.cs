using UnityEngine;

public class DialogInteractable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private DialogFrameScriptableObject DialogFrame;

    [Header("Debug")]
    [SerializeField] private InteractableController Source;

    public void OnInteract(InteractableController _source)
    {
        Source = _source;
        DialogController.Instance.LoadFrame(DialogFrame);
        PlayerController.Instance.SetPlayerCanMove(false);
        DialogController.Instance.OnDialogEnded.AddListener(OnDialogEnded);
    }

    public void OnDialogEnded(DialogController _)
    {
        PlayerController.Instance.SetPlayerCanMove(true);
        DialogController.Instance.OnDialogEnded.RemoveListener(OnDialogEnded);
        Source.EndInteraction();
    }
}