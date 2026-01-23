using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<InteractableController> OnInteract;
    [SerializeField] private UnityEvent<InteractableController> OnHover;
    [SerializeField] private UnityEvent<InteractableController> OnUnhover;

    [Header("Settings")]
    [SerializeField] private bool UnhoverOnInteract = true;

    [Header("Debug")]
    [SerializeField] private bool Hovered;
    [SerializeField] private bool LastHovered;
    [SerializeField] private int ActiveTasks = 0;

    private void Start() => gameObject.layer = LayerMask.NameToLayer("Interactable");

    public void SetHovered(bool _hovered) => Hovered = _hovered;

    private void Update()
    {
        if (Hovered != LastHovered)
        {
            if (Hovered) OnHover.Invoke(this);
            else OnUnhover.Invoke(this);

            LastHovered = Hovered;
        }
    }

    public void Interact()
    {
        if (OnInteract.GetPersistentEventCount() == 0)
        {
            Finish();
            return;
        }

        ActiveTasks = OnInteract.GetPersistentEventCount();
        OnInteract.Invoke(this);

        if (UnhoverOnInteract) SetHovered(false);
    }

    public void EndInteraction()
    {
        ActiveTasks--;
        if (ActiveTasks <= 0) Finish();
    }

    private void Finish()
    {
        InteractController.Instance.NotifyInteractionComplete();
    }
}