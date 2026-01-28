using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _Animator;
    [SerializeField] private Transform Target;
    [SerializeField] private NavMeshAgent Agent;

    [Header("Debug")]
    [SerializeField] private float Direction;
    [SerializeField] private bool Attacking;
    [SerializeField] private Vector3 LastPosition;

    private void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    private void Update()
    {
        Agent.SetDestination(Target.position);

        Vector3 movement = transform.position - LastPosition;
        Direction = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        _Animator.SetFloat("direction", Direction);
        _Animator.SetFloat("attacking", Attacking ? 1 : 0);

        LastPosition = transform.position;
    }
}
