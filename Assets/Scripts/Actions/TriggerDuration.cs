public class TriggerDuration
{
    public System.Action OnTriggeredEvent;
    public float duration;
    private float tempDuration;

    public TriggerDuration(float duration)
    {
        this.duration = duration;
        this.tempDuration = duration;
        OnTriggeredEvent += OnTriggered;
    }
    ~TriggerDuration()
    {
        OnTriggeredEvent -= OnTriggered;
    }

    private void OnTriggered()
    {
        duration = tempDuration + duration;
    }

    public void UpdateTick(float deltaTime)
    {
        duration -= deltaTime;
        if(duration<0)
        {
            OnTriggeredEvent?.Invoke();
        }
    }
    public void ResetTriggerDuration()
    {
        
    }
}