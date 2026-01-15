using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool Hovered;

    public void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void Update()
    {
        if(Hovered)
        {
        }
        else
        {
        }
    }

    public void SetHovered(bool _Hovered) => Hovered = _Hovered;

    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        InteractController.Instance.NotifyInteractionComplete();
    }
}
