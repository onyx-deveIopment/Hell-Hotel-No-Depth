using UnityEngine;
using UnityEngine.Events;

public class TimeWaster : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] public UnityEvent<TimeWaster> OnTimeWasted;

    [Header("Debug")]
    [SerializeField] private float Timer;

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0) OnTimeWasted.Invoke(this);
    }

    public void WasteTime(float _timer) => Timer = _timer;
}
