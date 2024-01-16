using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoopManager : Singleton<GameLoopManager>
{
    [SerializeField] private BattleState battleManager;
    [SerializeField] private ShopState shopState;
    [SerializeField] LoopClassBase currState;
    LoopClassBase CurrState{
        get => currState;
        
        set{
            currState = value;
            CurrState.AssignNeighbourBonusesState();
            currState.StartState();
        }
    }
    public void ChangeState(LoopClassBase loopClassBase)
    {
        CurrState = loopClassBase;
    }
    private void Start() {
        CurrState.AssignNeighbourBonusesState();
        CurrState.StartState();
    }
    private void Update() {
        CurrState.UpdateState();
    }
}
