using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDurationMono : ActionBase
{
    [SerializeField] private float duration;
    TriggerDuration triggerDuration;
    private void Awake() {
        if(triggerDuration == null)
        {

            triggerDuration = new TriggerDuration(duration);
        }
    }
    private void Update() {
        // if(BattleManager.Instance.isStartBattle)
        // {
            triggerDuration.UpdateTick(Time.deltaTime);
        // }
    }
    public override void AddFuncTriggerDuration(System.Action action)
    {
        if(triggerDuration == null)
        {

            triggerDuration = new TriggerDuration(duration);
        }
        triggerDuration.OnTriggeredEvent += action;
    }
    public override void RemoveFuncTriggerDuration(System.Action action)
    {
        triggerDuration.OnTriggeredEvent -= action;
    }
    
}
