using UnityEngine;

public class InteractableCollectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string ItemName;

    public void OnCollect(Interactable source)
    {
        InventoryController.Instance.AddToInventory(ItemName);
        
        source.EndInteraction();
        Destroy(gameObject);
    }
}