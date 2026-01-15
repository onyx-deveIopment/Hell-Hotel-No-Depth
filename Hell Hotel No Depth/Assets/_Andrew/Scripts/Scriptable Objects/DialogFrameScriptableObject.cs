using UnityEngine;

[CreateAssetMenu(fileName = "DialogFrame", menuName = "Dialog/DialogFrame")]
public class DialogFrameScriptableObject : ScriptableObject
{
    [Header("Text Settings")]
    [SerializeField] private string SpeakerName;
    [SerializeField][TextArea(3, 10)] private string DialogText;

    [Header("Options")]
    [SerializeField] private string OptionOne;
    [SerializeField] private string OptionTwo;
    [SerializeField] private string OptionThree;
    [SerializeField] private string OptionFour;

    [Header("Next Frames")]
    [SerializeField] private DialogFrameScriptableObject[] NextFrames;
}
