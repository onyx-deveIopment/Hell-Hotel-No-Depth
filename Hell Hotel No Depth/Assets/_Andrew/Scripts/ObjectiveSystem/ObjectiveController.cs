using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [HideInInspector] public static ObjectiveController Instance;

    [Header("References")]
    [SerializeField] private GameObject Prefab;
    [SerializeField] private ObjectiveScriptableObject TestObjective;

    [Header("Settings")]
    [SerializeField] private bool SpawnTestObjectiveOnStart;
    [SerializeField] private bool ForceTestObjective;

    private void Awake() => Instance = this;

    private void Start()
    {
        if (SpawnTestObjectiveOnStart) Popup(TestObjective);
    }

    private void Update()
    {
        if(!ForceTestObjective) return;
        ForceTestObjective = false;
        Popup(TestObjective);
    }

    public void Popup(ObjectiveScriptableObject _objective)
    {
        ObjectivePopupController objectivePopup = Instantiate(Prefab, transform).GetComponent<ObjectivePopupController>();
        objectivePopup.StartObjectivePopup(_objective);
    }
}
