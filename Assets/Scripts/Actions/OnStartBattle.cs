using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartBattle : ActionBase
{
    public override void AddFuncTriggerDuration(Action action)
    {
        BattleState.Instance.OnStartBattle += action;
    }
    public override void RemoveFuncTriggerDuration(Action action)
    {
        BattleState.Instance.OnStartBattle -= action;
    }
}
