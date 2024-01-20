using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMono : ItemMono
{
    ActionBase actionBase;
    public Weapon weapon;
    [SerializeField] private int damage;
    private void Start() {
        item = new Weapon(damage);
        weapon = (Weapon) item;
        actionBase = GetComponent<ActionBase>();
        actionBase.AddFuncTriggerDuration(()=>weapon.OnAttack());
        
    }
    private void OnDisable() {
        // actionBase.RemoveFuncTriggerDuration(()=>weapon.OnAttack());
    }
    public override void OnTriggerEnter()
    {
        base.OnTriggerEnter();
    }
    public override void OnTriggerExit()
    {
        base.OnTriggerExit();
    }
}
