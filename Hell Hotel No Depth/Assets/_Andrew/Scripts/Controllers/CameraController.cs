using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public static CameraController Instance;

    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private float SmoothSpeed = 10;

    private void Awake() => Instance = this;

    private void LateUpdate() => transform.position = Vector2.Lerp(transform.position, target.position, SmoothSpeed * Time.deltaTime);
}
