using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<InteractableController> OnInteract;

    [Header("Debug")]
    [SerializeField] private bool Hovered;
    [SerializeField] private int activeTasks = 0;

    public void Start() => gameObject.layer = LayerMask.NameToLayer("Interactable");

    public void SetHovered(bool _hovered) => Hovered = _hovered;

    public void Interact()
    {
        if (OnInteract.GetPersistentEventCount() == 0)
        {
            Finish();
            return;
        }

        activeTasks = OnInteract.GetPersistentEventCount();
        OnInteract.Invoke(this);
    }

    public void EndInteraction()
    {
        activeTasks--;
        if (activeTasks <= 0) Finish();
    }

    private void Finish()
    {
        InteractController.Instance.NotifyInteractionComplete();
    }
}