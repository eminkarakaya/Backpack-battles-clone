using UnityEngine;

public interface IDragAndDropableBase
{
    /// <summary>
    /// 0 -> 0 Degree
    /// 1 -> +90 Degree
    /// 2 -> +180 Degree
    /// 3 -> +270 Degree
    /// </summary>
    /// <value></value>
    
    public void RotateCounterClockwise90Degree();
    public void RotateClockwise90Degree();
    public byte RotateStage { get; set; }
    public void OnDrag();
    public void OnTouchEnd();
    public void Select();
    public void OnPointerEnter();
    public void OnPointerExit();

}
public interface IEnvantable : IDragAndDropableBase
{
    public bool IsInvolveAnyItem();
    public ISlotable [] TakeOffSlotMap();
    public void PutInSlotMap();


}
public interface IDragAndDropable : IDragAndDropableBase
{
    
}

public interface ISlot
{
    public void OnPointerExitWhileSelectedObject();
    public void OnPointerEnterWhileSelectedObject();
}

public interface ISlotForEnvanter :ISlot
{
   
}
public interface ISlotForItem:ISlot
{
   
}
public interface ISlotForGrid:ISlot
{
   
}


// Inputs
public interface IInputItem
{
    public bool CheckInput();
    public IDragAndDropable SelectClickable(int layerMask);
    public bool UnSelectClickable();
    public void UpdateTick();
    public bool CheckClickable(int layerMask,out IDragAndDropable dragAndDropable);
}
public interface IInputEnvanterItem
{
    
    public bool CheckInput();
    public IEnvantable SelectClickable(int layerMask);
    public bool UnSelectClickable();
    public void UpdateTick();
    public bool CheckClickable(int layerMask,out IEnvantable envantable);
    public ISlotForEnvanter OnPointerSlotable(int layerMask);

}
public interface IInputSlotMap
{
    public ISlotForGrid OnPointerSlotable(int layerMask);
}



public interface ISlotable
{
    public Grid [] GetGrids();
    public void TakeOffSlot();
}

public interface NeighbourBonusable
{
    
}