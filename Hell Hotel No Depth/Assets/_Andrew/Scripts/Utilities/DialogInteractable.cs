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
        DialogController.Instance.OnDialogEnded.AddListener(OnDialogEnded);
        PlayerController.Instance.SetPlayerCanMove(false);
    }

    public void OnDialogEnded(DialogController _)
    {
        PlayerController.Instance.SetPlayerCanMove(true);
        Source.EndInteraction();
    }
}