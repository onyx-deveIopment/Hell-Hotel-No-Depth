using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Target;
    [SerializeField] private NavMeshAgent Agent;

    private void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    private void Update()
    {
        Agent.SetDestination(Target.position);
    }
}
