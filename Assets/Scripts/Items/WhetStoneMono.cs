using UnityEngine;
public class WhetStoneMono : ItemMono
{
    public WhetStone whetStone;
    private void Start() {
        item = new WhetStone();
        whetStone = (WhetStone) item; 
    }
}
