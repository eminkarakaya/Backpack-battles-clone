using UnityEngine;
public class InputManager
{
    ISlotable slotable;

    IClickable clickable;
    ISlotable Slotable{get=>slotable;
    set{
        if(value == null && clickable != null)
        {
            if(slotable != null)
            {
                slotable.OnPointerExit();
                slotable = value;
            }
            return;
        }
        if(value != slotable)
        {       
            if(clickable != null)
            {
                if(slotable != null)
                    slotable.OnPointerExit();
                slotable = value;
                if(slotable != null)
                {
                    slotable.OnPointerEnter();
                }
            }
            return;
        }
        slotable = value;
    }}
    public IClickable Clickable {
        get=>clickable;
        set
        {
            clickable = value;
            
            if(clickable != null)
                clickable.Select();
        }
    }
    IInput Input;
    public void Initialize()
    {
        Input = new CastRayClass(OnTouchBegan,OnTouching,OnTouchEnd);
    }
    public void UpdateTick(int layerMask,int gridLayerMask)
    {   
        if(Input.UnSelectClickable())
        {
            Clickable = null;
        }
        if (Input.CheckClickable(layerMask))
        {
            Clickable = Input.SelectClickable(layerMask);
        }
        Slotable = Input.OnPointerSlotable(gridLayerMask);
        Input.UpdateTick();
    }
    public void OnTouchBegan()
    {
        if(Clickable != null)
            Clickable.Select();
    }
    public void OnTouching()
    {
        if(Clickable != null)
            Clickable.OnDrag();
    }
    public void OnTouchEnd()
    {
        if(Clickable != null)
        {
            if(Slotable != null)
            {
                Slotable.OnPointerExit();
                Slotable = null;
            }
            Clickable.OnTouchEnd();
        }
    }
}
