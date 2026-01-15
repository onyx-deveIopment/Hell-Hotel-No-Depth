using UnityEngine;

public class InteractableCollectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string ItemName;

    public void OnCollect(InteractableController _source)
    {
        InventoryController.Instance.AddToInventory(ItemName);
        
        _source.EndInteraction();
        Destroy(gameObject);
    }
}