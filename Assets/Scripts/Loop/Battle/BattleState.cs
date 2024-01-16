using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BattleState : LoopClassBase
{
    public static BattleState Instance;
    private void Awake() {
        Instance = this;
    }
    
    [SerializeField] private float normalBattleDur;
    [SerializeField] private float passedTime;
    public Action OnAssignNeighboursBuff;
    public Action OnStartBattle;
    public Action OnEndBattle;


    [ContextMenu("StartBattle")]
    public void StartBattle() {
        OnStartBattle?.Invoke();
        passedTime = 0f;
        Debug.Log("Start Battle");
    }
    public void EndBattle()
    {
        OnEndBattle?.Invoke();
    }

    public override void StartState()
    {
        StartBattle();
    }

    public override void UpdateState()
    {
        passedTime += Time.deltaTime;
        if(passedTime > normalBattleDur)
        {
            GameLoopManager.Instance.ChangeState(nextState);
        }
    }

    public override void AssignNeighbourBonusesState()
    {
        Debug.Log("AssignNeighbourBonusesState");
        OnAssignNeighboursBuff?.Invoke();
    }
}
