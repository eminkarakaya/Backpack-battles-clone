public class WhetStone : Item
{
    Weapon [] weapons;
    NeighbourBonus<Weapon> neighbourBonus;
    public override void Execute()
    {
        foreach (var item in weapons)
        {
            item.damage ++;
        }
    } 
    public WhetStone()
    {
        neighbourBonus = new NeighbourBonusWhetStone();
    }
}