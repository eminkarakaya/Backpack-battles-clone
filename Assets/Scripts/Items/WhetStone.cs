public class WhetStone : Item
{
    NeighbourBonus<Weapon> neighbourBonus;

    private void Start() {
        
    }
    public override void Execute()
    {
        
    } 
    public WhetStone()
    {
        neighbourBonus = new NeighbourBonusWhetStone();
    }
}