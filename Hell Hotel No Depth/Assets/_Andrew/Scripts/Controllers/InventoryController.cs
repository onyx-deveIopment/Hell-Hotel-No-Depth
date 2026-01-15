using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] public static InventoryController Instance;

    [Header("Debug")]
    [SerializeField] private List<string> Inventory = new List<string>();

    private void Awake() => Instance = this;

    public void AddToInventory(string _Item) => Inventory.Add(_Item);
    public void RemoveFromInventory(string _Item) => Inventory.Remove(_Item);
    public bool DoesInventoryHaveItem(string _Item) => Inventory.Contains(_Item);
}
