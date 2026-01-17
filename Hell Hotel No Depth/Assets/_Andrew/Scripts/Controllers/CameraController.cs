using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public static CameraController Instance;

    [Header("References")]
    [SerializeField] private List<Transform> Targets;

    [Header("Settings")]
    [SerializeField] private float SmoothSpeed = 10;

    [Header("Debug")]
    [SerializeField] private float Quicked = 0;

    private void Awake() => Instance = this;

    private void LateUpdate()
    {
        if (Quicked > 0)
        {
            transform.position = GetAveragePosition();
            Quicked -= 1;
            return;
        }

        transform.position = Vector2.Lerp(transform.position, GetAveragePosition(), SmoothSpeed * Time.deltaTime);
    }

    private Vector2 GetAveragePosition()
    {
        Vector2 average = Vector2.zero;
        foreach (Transform target in Targets)
        {
            average += (Vector2)target.position;
        }
        return average / Targets.Count;
    }

    public void Quick(int _frames) => Quicked = _frames;

    public void AddTarget(Transform _target) => Targets.Add(_target);
    public void RemoveTarget(Transform _target) => Targets.Remove(_target);
}