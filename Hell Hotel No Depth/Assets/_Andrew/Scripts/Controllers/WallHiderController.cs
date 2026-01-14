using UnityEngine;
using UnityEngine.Tilemaps;

public class WallHiderController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private TilemapRenderer _TilemapRenderer;

    private void Start() => _TilemapRenderer.enabled = false;
}
