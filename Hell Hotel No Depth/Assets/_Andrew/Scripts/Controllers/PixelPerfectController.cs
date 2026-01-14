using UnityEngine;

public class PixelPerfectController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Transform ReferencePosition;

    [Header ("Settings")]
    [SerializeField] private float PixelsPerUnit = 32;
    [SerializeField] private float Smoothing = 10;

    [Header ("Debug")]
    [SerializeField] private Vector3 Offset;

    private void Start()
    {
        Offset = transform.position - ReferencePosition.position;
    }

    private void LateUpdate()
    {
        Vector3 position = ReferencePosition.position;
        position.x = Mathf.Round(position.x * PixelsPerUnit) / PixelsPerUnit;
        position.y = Mathf.Round(position.y * PixelsPerUnit) / PixelsPerUnit;
        transform.position = Vector3.Lerp(transform.position, position + Offset, Smoothing * Time.deltaTime);
    }
}
