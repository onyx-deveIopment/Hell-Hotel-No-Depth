using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _Animator;
    [SerializeField] private Transform Target;
    [SerializeField] private NavMeshAgent Agent;

    [Header("Settings")]
    [SerializeField] private bool TargetPlayer = true;
    [SerializeField] private float AttackDistance = 1;
    [SerializeField] private float AttackCooldown = 1;
    [SerializeField] private float AttackDamage = 5;

    [Header("Debug")]
    [SerializeField] private float Direction;
    [SerializeField] private float LastDirection;
    [SerializeField] private bool Attack;
    [SerializeField] private float AttackCounter;
    [SerializeField] private Vector3 LastPosition;

    private void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        if(TargetPlayer) Target = PlayerController.Instance.GetPlayer();
    }

    private void Update()
    {
        Attack = Vector2.Distance(Target.position, transform.position) <= AttackDistance;

        if(Attack)
        {
            AttackCounter -= Time.deltaTime;
            if(AttackCounter <= 0)
            {
                AttackCounter = AttackCooldown;
                HealthController.Instance.TakeDamage(AttackDamage);
            }
        }

        Agent.SetDestination(Attack ? transform.position : Target.position);

        Vector3 movement = transform.position - LastPosition;
        Direction = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        if(!Attack) _Animator.SetFloat("direction", Direction);
        _Animator.SetFloat("attack", Attack ? 1 : 0);

        LastPosition = transform.position;
        LastDirection = Direction;
    }
}
