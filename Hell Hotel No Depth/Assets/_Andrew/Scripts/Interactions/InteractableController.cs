using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class InteractableController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<SpriteRenderer> OutlineObjects;

    [Header("Events")]
    [SerializeField] private UnityEvent<InteractableController> OnInteract;

    [Header("Debug")]
    [SerializeField] private bool Hovered;
    [SerializeField] private int ActiveTasks = 0;

    private void Start() => gameObject.layer = LayerMask.NameToLayer("Interactable");

    public void SetHovered(bool _hovered) => Hovered = _hovered;

    private void Update()
    {
        foreach (var outlineObject in OutlineObjects)
        {
            outlineObject.material.SetFloat("_Outline", Hovered && ActiveTasks <= 0 ? 1 : 0);
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