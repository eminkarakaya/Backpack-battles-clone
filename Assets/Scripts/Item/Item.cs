using UnityEngine;

public class Item 
{
    public ItemType itemType;
    public virtual void Execute()
    {
        
    }   
}
public class Food : Item
{
    public override void Execute()
    {

    }   
    
}
public class WeaponStats
{
    public Vector2 damageInterval;
    public float cooldown;
}
public class Weapon : Item
{
    // stats
    public Weapon(int damage)
    {
        this.damage = damage; 
    }
    public int damage;
    public void OnAttack()
    {
        Debug.Log("Attack " + this.ToString() + " dealed damage = "+damage);
    }
}
