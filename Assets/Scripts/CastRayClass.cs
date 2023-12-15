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

    public bool CheckInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool CheckClickable(int layerMask, out IEnvantable envantable1)
    {
        RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
        if(hit.collider == null)
        {
            envantable1 = null;
            return false;
        } 
        if(hit.collider.TryGetComponent(out envantable))
        {
            
            envantable1 = envantable;
            return true;
             // eger envanterde herhangı bı ıtem varsa secemıyorz.
        }
        envantable1 = null;
        return false;
    }
}
public class CastRayClassForItem : IInputItem
{
    public CastRayClassForItem(Action OnTouchBegan,Action OnTouching,Action OnTouchEnd,Action OnRotate)
    {
        this.OnTouchBegan = OnTouchBegan;
        this.OnTouchEnd = OnTouchEnd;
        this.OnTouching = OnTouching;
        this.OnRotate = OnRotate;
    }

    // public CastRayClassForItem(Action onTouchBegan, Action onTouching, Action onTouchEndItem)
    // {
    //     OnTouchBegan = onTouchBegan;
    //     OnTouching = onTouching;
    //     this.onTouchEndItem = onTouchEndItem;
    // }

    public Action OnTouchBegan;
    public Action OnTouching;
    public Action OnTouchEnd;
    public Action OnRotate;
    IDragAndDropable clickable;
    private Action onTouchEndItem;

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
    public bool CheckInput()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool CheckClickable(int layerMask,out IDragAndDropable dragAndDropable)
    {
        RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
        if(hit.collider == null) 
        {
            dragAndDropable = null;
            return false;
        }
        if(hit.collider.TryGetComponent(out clickable))
        {    
            dragAndDropable = clickable;
            return true;
        }
        dragAndDropable = null;
        return false;
    }
    public void UpdateTick()
    {
        if(Input.GetMouseButton(0))
        {
            OnTouching?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            OnRotate?.Invoke();
        }
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

    
}