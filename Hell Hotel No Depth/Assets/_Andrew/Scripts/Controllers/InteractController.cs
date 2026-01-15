using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    [HideInInspector] public static InteractController Instance;

    [Header("References")]
    [SerializeField] private PlayerController _PlayerController;

    [Header("Settings")]
    [SerializeField] private float Reach = 1;

    [Header("Debug")]
    [SerializeField] private Interactable HoveredInteractable;
    [SerializeField] private bool InteractInputQueued;
    [SerializeField] private bool AwaitingInteracCompletion;

    private void Awake() => Instance = this;

    private void Update()
    {
        AttemptInteract();
        InteractInputQueued = false;
    }

    private void AttemptInteract()
    {
        if (AwaitingInteracCompletion) return;

        Vector2 direction = new Vector2(Mathf.Cos(_PlayerController.GetDirection() * Mathf.Deg2Rad), Mathf.Sin(_PlayerController.GetDirection() * Mathf.Deg2Rad));
        Vector2 origin = _PlayerController.transform.position;
        Vector2 target = origin + direction.normalized * Reach;

        RaycastHit2D hit = Physics2D.Linecast(origin, target, 1 << LayerMask.NameToLayer("Interactable"));
        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                HoveredInteractable = interactable;
                HoveredInteractable.SetHovered(true);
            }
        }
        else
        {
            HoveredInteractable?.SetHovered(false);
            HoveredInteractable = null;
        }

        if (InteractInputQueued && HoveredInteractable != null)
        {
            AwaitingInteracCompletion = true;
            HoveredInteractable.Interact();
        }

        Debug.DrawLine(origin, target, Color.red);
    }

    public void NotifyInteractionComplete() => AwaitingInteracCompletion = false;

    public void InteractInputReceived(InputAction.CallbackContext context) { if (context.performed) InteractInputQueued = true; }
}
