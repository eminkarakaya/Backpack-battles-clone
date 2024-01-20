using UnityEngine;
public class WhetStoneMono : WeaponMono
{
    public WhetStone whetStone;
    private void Start() {
        item = new WhetStone();
        whetStone = (WhetStone) item; 
    }
}
