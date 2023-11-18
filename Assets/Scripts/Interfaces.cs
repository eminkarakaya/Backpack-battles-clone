using UnityEngine;
public interface IClickable
{
    public void OnDrag();
    public void OnTouchEnd();
    public void Select();
}
public interface ISlotable
{
    public void OnPointerExit();
    public void OnPointerEnter();
}
public interface IInput
{
    public ISlotable OnPointerSlotable(int layerMask);
    public IClickable SelectClickable(int layerMask);
    public bool UnSelectClickable();
    public void UpdateTick();
    public bool CheckClickable(int layerMask);
    
}
public enum ItemType
{
    Grape,
    Banana
}