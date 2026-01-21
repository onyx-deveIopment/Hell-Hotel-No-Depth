using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _Animator;
    [SerializeField] private BoxCollider2D AboveCollider;
    [SerializeField] private BoxCollider2D BelowCollider;

    [Header("Settings")]
    [SerializeField] private float PlayerBelowZPosition = 1;
    [SerializeField] private float PlayerAboveZPosition = -1;

    [Header("Debug")]
    [SerializeField] private bool PlayerAbove;
    [SerializeField] private float Direction = 0;

    private void Update()
    {
        CheckPlayerPosition();

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            PlayerAbove ? PlayerAboveZPosition : PlayerBelowZPosition
        );

        _Animator.SetInteger("direction", (int)Direction);

        AboveCollider.enabled = Direction == 0 && PlayerAbove;
        BelowCollider.enabled = Direction == 0 && !PlayerAbove;
    }

    private void CheckPlayerPosition() => PlayerAbove = PlayerController.Instance.GetPosition().y > transform.position.y;

    public void OnHovered(InteractableController _) => _Animator.SetFloat("hovered", 1);
    public void OnUnhovered(InteractableController _) => _Animator.SetFloat("hovered", 0);

    public void OnInteract(InteractableController _interactableController)
    {
        CheckPlayerPosition();
        Direction = PlayerAbove ? -90 : 90;
    }
}
