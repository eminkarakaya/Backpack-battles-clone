using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoopClassBase : MonoBehaviour
{
    public LoopClassBase nextState;
    public abstract void AssignNeighbourBonusesState();
    public abstract void StartState();
    public abstract void UpdateState();
}