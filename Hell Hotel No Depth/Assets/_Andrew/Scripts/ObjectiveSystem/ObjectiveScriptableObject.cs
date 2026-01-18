using UnityEngine;

[CreateAssetMenu(fileName = "NewObjective", menuName = "Objective System/Objective")]
public class ObjectiveScriptableObject : ScriptableObject
{
    [SerializeField] public string Text;
}
