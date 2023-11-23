using System;
using UnityEngine;
public class CastRayClassForSlotMap : IInputSlotMap
{
    public ISlotForGrid OnPointerSlotable(int layerMask)
    {
        RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
        if(hit.collider == null) return null;
        if(hit.collider.TryGetComponent(out ISlotForGrid slotable))
        {
            
            return slotable;
        }
        return null;
    }
}
public class CastRayClassForEnvanterItem : IInputEnvanterItem
{
    IEnvantable envantable;
    public Action OnTouchBegan;
    public Action OnTouching;
    public Action OnTouchEnd;
    public CastRayClassForEnvanterItem(Action OnTouchBegan,Action OnTouching,Action OnTouchEnd)
    {
        this.OnTouchBegan = OnTouchBegan;
        this.OnTouchEnd = OnTouchEnd;
        this.OnTouching = OnTouching;
    }

    public bool CheckClickable(int layerMask)
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
            if(hit.collider == null) return false;
            if(hit.collider.TryGetComponent(out envantable))
            {
                return true;
            }
        }
        return false;
    }

    public IEnvantable SelectClickable(int layerMask)
    {
        RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
        if(hit.collider == null) return null;
        if(hit.collider.TryGetComponent(out envantable))
        {
            OnTouchBegan?.Invoke();
            return envantable;
        }
    
        return null;
    }

    public bool UnSelectClickable()
    {
        if(Input.GetMouseButtonUp(0))
        {
            
            OnTouchEnd?.Invoke();
            return true;
        }
        return false;
    }

    public void UpdateTick()
    {
        if(Input.GetMouseButton(0))
        {
            OnTouching?.Invoke();
        }
    }
    public ISlotForEnvanter OnPointerSlotable(int layerMask)
    {
        RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
        if(hit.collider == null) 
        {
            return null;
        }

        if(hit.collider.TryGetComponent(out ISlotForEnvanter slotForEnvanter))
        {
            return slotForEnvanter;
        }
        return null;
    }
}
public class CastRayClassForItem : IInputItem
{
    public CastRayClassForItem(Action OnTouchBegan,Action OnTouching,Action OnTouchEnd)
    {
        this.OnTouchBegan = OnTouchBegan;
        this.OnTouchEnd = OnTouchEnd;
        this.OnTouching = OnTouching;
    }

    public Action OnTouchBegan;
    public Action OnTouching;
    public Action OnTouchEnd;
    IDragAndDropable clickable;
    
    public IDragAndDropable SelectClickable(int layerMask)
    {
        RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
        if(hit.collider == null) return null;
        if(hit.collider.TryGetComponent(out clickable))
        {
            OnTouchBegan?.Invoke();
            return clickable;
        }
    
        return null;
    }
    public bool CheckClickable(int layerMask)
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
            if(hit.collider == null) return false;
            if(hit.collider.TryGetComponent(out clickable))
            {
                return true;
            }
        }
        return false;
    }
    public void UpdateTick()
    {
        if(Input.GetMouseButton(0))
        {
            OnTouching?.Invoke();
        }
    }

    public bool UnSelectClickable()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(clickable != null)
                clickable.EndPos = Input.mousePosition;
            OnTouchEnd?.Invoke();
            return true;
        }
        return false;
    }

    
}