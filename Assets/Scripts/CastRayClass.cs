using UnityEngine;

public class CastRayClass : IInput
{
    
    public CastRayClass(System.Action OnTouchBegan,System.Action OnTouching,System.Action OnTouchEnd)
    {
        this.OnTouchBegan = OnTouchBegan;
        this.OnTouchEnd = OnTouchEnd;
        this.OnTouching = OnTouching;
    }

    public System.Action OnTouchBegan;
    public System.Action OnTouching;
    public System.Action OnTouchEnd;
    IClickable clickable;
    
    public IClickable SelectClickable(int layerMask)
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
            OnTouchEnd?.Invoke();
            return true;
        }
        return false;
    }

    public ISlotable OnPointerSlotable(int layerMask)
    {
        RaycastHit2D hit = Utils.CastRay(Input.mousePosition,layerMask);
        if(hit.collider == null) return null;
        if(hit.collider.TryGetComponent(out ISlotable slotable))
        {
            
            return slotable;
        }
        return null;
    }
}