using UnityEngine;

[CreateAssetMenu(fileName = "DialogFrame", menuName = "Dialog/DialogFrame")]
public class DialogFrameScriptableObject : ScriptableObject
{
    [Header("Portrait")]
    [SerializeField] public Sprite Portrait;

    [Header("Text Settings")]
    [SerializeField] public string SpeakerName;
    [SerializeField][TextArea(3, 10)] public string DialogText;

    [Header("Options")]
    [SerializeField] public string OptionOne;
    [SerializeField] public string OptionTwo;
    [SerializeField] public string OptionThree;
    [SerializeField] public string OptionFour;

    [Header("Next Frames")]
    [SerializeField] public DialogFrameScriptableObject[] NextFrames;

    [Header("Audio")]
    [SerializeField] public AudioClip DialogAudioClip;
}
