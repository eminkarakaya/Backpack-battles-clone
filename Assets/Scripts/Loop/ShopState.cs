using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ShopState : LoopClassBase
{
    
    public static ShopState Instance;
    private void Awake() {
        Instance = this;
    }
    public Action OnShopOpened;
    public void FindBattleButton()
    {
        GameLoopManager.Instance.ChangeState(nextState);
    }

    public override void StartState()
    {
        OnShopOpened?.Invoke();       
    }

    public override void UpdateState()
    {
        
    }

    public override void AssignNeighbourBonusesState()
    {
        
    }
}
