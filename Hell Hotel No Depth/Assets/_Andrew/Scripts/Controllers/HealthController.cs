using UnityEngine;

public class HealthController : MonoBehaviour
{
    [HideInInspector] public static HealthController Instance;

    [Header("Settings")]
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private float HealthRegenSpeed = 1;

    [Header("Debug")]
    [SerializeField] private float Health;

    private void Awake() => Instance = this;

    private void Start()
    {
        Health = MaxHealth;
    }

    private void Update()
    {
        if(Health <= 0) Debug.Log("PLAYER DEAD");

        Health = Mathf.Clamp(Health + (HealthRegenSpeed * Time.deltaTime), 0, 100);
    }

    public void TakeDamage(float _damage) => Health -= _damage;
}
