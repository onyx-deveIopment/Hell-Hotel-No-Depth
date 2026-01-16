using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogController : MonoBehaviour
{
    [HideInInspector] public static DialogController Instance;

    [Header("References")]
    [SerializeField] private GameObject Pannel;
    [Space]
    [SerializeField] private Image PortraitImage;
    [SerializeField] private Text SpeakerNameText;
    [SerializeField] private Text DialogText;
    [Space]
    [SerializeField] private GameObject ButtonOne;
    [SerializeField] private Text ButtonOneText;
    [SerializeField] private GameObject ButtonTwo;
    [SerializeField] private Text ButtonTwoText;
    [SerializeField] private GameObject ButtonThree;
    [SerializeField] private Text ButtonThreeText;
    [SerializeField] private GameObject ButtonFour;
    [SerializeField] private Text ButtonFourText;
    [Space]
    [SerializeField] private AudioSource _AudioSource;
    [Space]
    [SerializeField] private DialogFrameScriptableObject TestDialogFrame;

    [Header("Events")]
    [SerializeField] public UnityEvent<DialogController> OnDialogEnded;

    [Header("Debug")]
    [SerializeField] private DialogFrameScriptableObject CurrentDialogFrame;
    [Space]
    [SerializeField] private bool LoadTestDialogOnStart = false;

    private void Awake() => Instance = this;

    private void Start()
    {
        Pannel.SetActive(false);
        if (LoadTestDialogOnStart) LoadFrame(TestDialogFrame);
    }

    public void LoadFrame(DialogFrameScriptableObject _frame)
    {
        _AudioSource.Stop();

        if (_frame == null)
        {
            Pannel.SetActive(false);
            OnDialogEnded.Invoke(null);
            return;
        }

        Pannel.SetActive(true);

        CurrentDialogFrame = _frame;
        PortraitImage.sprite = CurrentDialogFrame.Portrait;
        SpeakerNameText.text = CurrentDialogFrame.SpeakerName;
        DialogText.text = CurrentDialogFrame.DialogText;

        ButtonOne.GetComponent<Button>().Select();

        ButtonOne.SetActive(CurrentDialogFrame.OptionOne != "");
        ButtonOneText.text = CurrentDialogFrame.OptionOne;
        ButtonTwo.SetActive(CurrentDialogFrame.OptionTwo != "");
        ButtonTwoText.text = CurrentDialogFrame.OptionTwo;
        ButtonThree.SetActive(CurrentDialogFrame.OptionThree != "");
        ButtonThreeText.text = CurrentDialogFrame.OptionThree;
        ButtonFour.SetActive(CurrentDialogFrame.OptionFour != "");
        ButtonFourText.text = CurrentDialogFrame.OptionFour;

        _AudioSource.clip = CurrentDialogFrame.DialogAudioClip;
        _AudioSource.Play();
    }

    private void NextFrame(int _buttonIndex)
    {
        LoadFrame(CurrentDialogFrame.NextFrames.Length > _buttonIndex && CurrentDialogFrame.NextFrames[_buttonIndex] != null ? CurrentDialogFrame.NextFrames[_buttonIndex] : null);
    }

    public void ButtonPressed(int _buttonIndex) => NextFrame(_buttonIndex);
}