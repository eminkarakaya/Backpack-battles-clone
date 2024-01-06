using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class ActionBase : MonoBehaviour
{
    public Action OnTrigger;
    public abstract void AddFuncTriggerDuration(System.Action action);
    public abstract void RemoveFuncTriggerDuration(System.Action action);
}
