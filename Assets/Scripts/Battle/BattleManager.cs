using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private float normalBattleDur;
    public bool isStartBattle;
    public Action OnStartBattle;
    public Action OnEndBattle;
    [ContextMenu("StartBattle")]
    public void StartBattle() {
        OnStartBattle?.Invoke();
        Debug.Log("Start Battle");
    }
    public void EndBattle()
    {
        OnEndBattle?.Invoke();
    }
    private void Update() {
        if(isStartBattle == false)
        {
            return;
        }
        
    }   
}
