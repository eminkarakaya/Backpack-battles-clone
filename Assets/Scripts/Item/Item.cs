using UnityEngine;

public class Item 
{
    public string itemName;
    public ItemType itemType;
    public Prevalence prevalence;
    public virtual void OnTriggerEnter()
    {
        HoverTip.Instance.OpenTipObj();
        HoverTip.Instance.SetItemTip(this);

    }   
    public virtual void OnTriggerExit()
    {
        HoverTip.Instance.CloseTipObj();
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
