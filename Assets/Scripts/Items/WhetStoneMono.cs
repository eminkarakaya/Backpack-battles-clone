using UnityEngine;
public class WhetStoneMono : WeaponMono
{
    public WhetStone whetStone;
    private void Start() {
        whetStone = new WhetStone();
    }
    public override void Execute()
    {
        whetStone.Execute(); 
    }
}
