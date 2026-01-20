using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _Animator;
    [SerializeField] private BoxCollider2D _BoxCollider2D;

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

        _Animator.SetFloat("direction", Direction);
        _BoxCollider2D.enabled = Direction == 0;
    }

    private void CheckPlayerPosition() => PlayerAbove = PlayerController.Instance.GetPosition().y > transform.position.y;

    public void OnInteract(InteractableController _interactableController)
    {
        CheckPlayerPosition();
        Direction = PlayerAbove ? -90 : 90;
    }
}
