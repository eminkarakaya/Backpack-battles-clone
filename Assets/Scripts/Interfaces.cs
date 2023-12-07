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
    public void PutInSlotMap();
}
public interface IEnvantable : IDragAndDropableBase
{
    public bool IsInvolveAnyItem();
    public ISlotable [] TakeOffSlotMap();

}
public interface IDragAndDropable : IDragAndDropableBase
{
    public void TakeOffSlotMap();

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
    public Grid [] GetGrids();
    public void PutInSlot();
    public void TakeOffSlot();
}


public enum Direction3x1
{
    Middle,Right,Left
}
public enum Direction4
{
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}
public enum DirectionRightLeft
{
}
public enum DirectionUpDownRightLeft
{
    Right,Left,
    Up,Down
}