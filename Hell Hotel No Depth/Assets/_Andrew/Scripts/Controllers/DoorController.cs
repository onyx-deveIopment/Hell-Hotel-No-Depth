using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float PlayerBelowXPosition = 1;
    [SerializeField] private float PlayerAboveXPosition = -1;

    private void Update()
    {
        Vector2 playerPosition = PlayerController.Instance.GetPosition();
        Vector3 doorPosition = transform.position;

        if (playerPosition.y > doorPosition.y) doorPosition.z = PlayerAboveXPosition;
        if (playerPosition.y < doorPosition.y) doorPosition.z = PlayerBelowXPosition;

        transform.position = doorPosition;
    }
}
