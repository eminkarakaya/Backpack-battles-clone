using UnityEngine;
public interface IEnvantable 
{
    public void PutInSlotMap();
    public void PutInSlotMapError();
    public void TakeItOutSlotMap();
}
public interface IDragAndDropable
{
    public void OnDrag();
    public void OnTouchEnd();
    public void Select();
}

public interface ISlotForEnvanter
{
    public void OnPointerExit();
    public void OnPointerEnter();
}
public interface ISlotForItem
{
    public void OnPointerExit();
    public void OnPointerEnter();
}
public interface ISlotForGrid
{
    public void OnPointerExit();
    public void OnPointerEnter();
}


// Inputs
public interface IInputItem
{
    public IDragAndDropable SelectClickable(int layerMask);
    public bool UnSelectClickable();
    public void UpdateTick();
    public bool CheckClickable(int layerMask);
}
public interface IInputEnvanterItem
{
    public IEnvantable SelectClickable(int layerMask);
    public bool UnSelectClickable();
    public void UpdateTick();
    public bool CheckClickable(int layerMask);
    public ISlotForEnvanter OnPointerSlotable(int layerMask);

}
public interface IInputSlotMap
{
    public ISlotForGrid OnPointerSlotable(int layerMask);
}









public interface ISlotable
{
    public void PutInSlot();
}