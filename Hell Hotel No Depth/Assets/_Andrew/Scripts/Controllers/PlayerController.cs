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
    [SerializeField] private Vector2 Move;
    [SerializeField] private Vector2 LastPosition;

    private void Awake() => Instance = this;

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateGraphics();
        LastPosition = rigidBody.position;
    }

    private void MovePlayer()
    {
        if (!PlayerCanMove) return;
        Move = MoveInput * MoveSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + Move);
    }

    private void UpdateGraphics()
    {
        if (MoveInput != Vector2.zero && PlayerCanMove) Direction = Mathf.Atan2(MoveInput.y, MoveInput.x) * Mathf.Rad2Deg;

        animator.SetFloat("Direction", Direction);
        animator.SetFloat("IsMoving", (LastPosition - rigidBody.position).magnitude > 0f ? 1f : 0f);
    }

    public void MoveInputReceived(InputAction.CallbackContext _context) => MoveInput = _context.ReadValue<Vector2>();

    public void SetPlayerCanMove(bool _playerCanMove) => PlayerCanMove = _playerCanMove;
    public float GetDirection() => Direction;
}
