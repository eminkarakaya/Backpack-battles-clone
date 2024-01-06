using UnityEngine;

public abstract class NeighbourBonus<T> where T : Item
{
    public abstract void NeighbourBonusTrigger(T item);
}
public class NeighbourBonusWhetStone : NeighbourBonus<Weapon>
{
    public override void NeighbourBonusTrigger(Weapon item)
    {
        item.damage ++;
        Debug.Log(item.damage + " " +item.ToString());
    }
}
public enum BonusType
{
    IncreaseDamage,
    DecreaseCooldown,
}

