using UnityEngine;

public class Item 
{
    public string itemName;
    public ItemType itemType;
    public Prevalence prevalence;
    public virtual void OnTriggerEnter()
    {
        HoverTip.Instance.OpenTipObj();

    }   
    public virtual void OnTriggerExit()
    {
        HoverTip.Instance.CloseTipObj();
    }   
    public Item()
    {
        itemName = this.ToString();
    }
}
public class Food : Item
{
    
}
public class WeaponStats
{
    public Vector2 damageInterval;
    public float cooldown;
}
public class Weapon : Item
{
    // stats
    public Weapon(int damage,float cooldown,string itemName)
    {
        this.itemName = itemName;
        this.damage = damage; 
        this.cooldown = cooldown; 
    }
    public int damage;
    public float cooldown;
    public void OnAttack()
    {
        Debug.Log("Attack " + this.ToString() + " dealed damage = "+damage);
    }
}
