public class WhetStone : Item
{
    NeighbourBonus<Weapon> neighbourBonus;

    private void Start() {
        
    }
    public WhetStone()
    {
        neighbourBonus = new NeighbourBonusWhetStone();
        itemName = this.ToString();
        itemType = ItemType.Accessory;
        prevalence = Prevalence.Rare;
    }
}