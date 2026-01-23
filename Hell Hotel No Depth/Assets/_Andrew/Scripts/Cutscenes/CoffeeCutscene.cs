using UnityEngine;

public class CoffeeCutscene : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private InteractableController Source;

    public void OnInteract(InteractableController _interactableController)
    {
        Source = _interactableController;
    }
}
