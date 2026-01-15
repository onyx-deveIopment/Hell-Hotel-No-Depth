using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public static PlayerController Instance;

    [Header("References")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private float MoveSpeed = 5f;

    [Header("Debug")]
    [SerializeField] private bool PlayerCanMove = true;
    [SerializeField] private float Direction;
    [SerializeField] private Vector2 MoveInput;

    private void Awake() => Instance = this;

    private void FixedUpdate()
    {
        if (!PlayerCanMove) return;

        Vector2 move = MoveInput * MoveSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + move);

        if (MoveInput != Vector2.zero) Direction = Mathf.Atan2(MoveInput.y, MoveInput.x) * Mathf.Rad2Deg;

        animator.SetFloat("Direction", Direction);
        animator.SetFloat("IsMoving", move.magnitude > 0f ? 1f : 0f);
    }

    public void MoveInputReceived(InputAction.CallbackContext _context) => MoveInput = _context.ReadValue<Vector2>();

    public void SetPlayerCanMove(bool _playerCanMove) => PlayerCanMove = _playerCanMove;
    public float GetDirection() => Direction;
}
