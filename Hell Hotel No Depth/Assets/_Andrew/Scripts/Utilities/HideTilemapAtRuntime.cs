using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapRenderer))]
public class HideTilemapAtRuntime : MonoBehaviour
{
    private void Awake() => GetComponent<TilemapRenderer>().enabled = false;
}
