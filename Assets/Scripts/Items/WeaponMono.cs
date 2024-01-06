using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMono : ItemMono
{
    ActionBase actionBase;
    Weapon weapon;
    [SerializeField] private int damage;
    private void Start() {
        weapon = new Weapon(damage);
        actionBase = GetComponent<ActionBase>();
        actionBase.AddFuncTriggerDuration(()=>weapon.OnAttack());
        
    }
    private void OnDisable() {
        actionBase.RemoveFuncTriggerDuration(()=>weapon.OnAttack());
    }
}
