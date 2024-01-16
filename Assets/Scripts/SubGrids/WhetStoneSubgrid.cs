using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhetStoneSubgrid : MonoBehaviour
{
    [SerializeField] private int damageIncreaseCount;
    [SerializeField] public Weapon weapon;
    private void Start() {
        BattleState.Instance.OnAssignNeighboursBuff += AssignWeapon;
    }
    private void OnDisable() {
        BattleState.Instance.OnAssignNeighboursBuff -= AssignWeapon;
    }
    public void AssignWeapon()
    {
            Debug.Log("assignweapon");
        if(GetComponent<SubGridNeighbour>().selectedGrid != null && GetComponent<SubGridNeighbour>().selectedGrid.gridInEnvanter.gridInItem.itemDragAndDrop.TryGetComponent(out WeaponMono weaponMono))
        {
            Debug.Log(weapon.damage + " 111");
            weapon = weaponMono.weapon;
            GiveBuffWeapon();
            Debug.Log(weapon.damage + " 222");
        }
    }
    public void GiveBuffWeapon()
    {
        weapon.damage += damageIncreaseCount;
    }
}
