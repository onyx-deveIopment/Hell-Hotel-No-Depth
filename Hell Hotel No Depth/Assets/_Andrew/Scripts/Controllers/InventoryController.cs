using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] public static InventoryController Instance;

    [Header("Debug")]
    [SerializeField] private List<string> Inventory = new List<string>();

    private void Awake() => Instance = this;

    public void AddToInventory(string _item) => Inventory.Add(_item);
    public void RemoveFromInventory(string _item) => Inventory.Remove(_item);
    public bool DoesInventoryHaveItem(string _item) => Inventory.Contains(_item);
}
