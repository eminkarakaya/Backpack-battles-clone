public class InputManager
{
    ISlotForGrid slotable;
    ISlotForGrid Slotable{get=>slotable;
    set{
        if(value == null && Envantable != null)
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
            if(Envantable != null)
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

    ISlotForEnvanter slotForEnvanter;
    ISlotForEnvanter SlotForEnvanter{get=>slotForEnvanter;
        set{
            if(value == null && DragAndDropable != null)
            {
                if(slotForEnvanter != null)
                {
                    slotForEnvanter.OnPointerExit();
                    slotForEnvanter = value;
                }
                return;
            }
            if(value != slotForEnvanter)
            {       
                if(DragAndDropable != null)
                {
                    if(slotForEnvanter != null)
                        slotForEnvanter.OnPointerExit();
                    slotForEnvanter = value;
                    if(slotForEnvanter != null)
                    {
                        slotForEnvanter.OnPointerEnter();
                    }
                }
                return;
            }
            slotForEnvanter = value;
        }}
    ISlotForItem slotForItem;
    
    IDragAndDropable clickable;
    public IDragAndDropable DragAndDropable {
        get=>clickable;
        set
        {
            clickable = value;
            
            if(clickable != null)
                clickable.Select();
        }
    }
    IEnvantable envantable;

    IEnvantable Envantable{get=>envantable;
    set{
        envantable = value;
        // if(value == null && envantable != null)
        // {
        //     if(envantable != null)
        //     {
        //         envantable.OnPointerExit();
        //         envantable = value;
        //     }
        //     return;
        // }
        // if(value != envantable)
        // {       
        //     if(envantable != null)
        //     {
        //         if(envantable != null)
        //             envantable.OnPointerExit();
        //         envantable = value;
        //         if(envantable != null)
        //         {
        //             envantable.OnPointerEnter();
        //         }
        //     }
        //     return;
        // }
        // envantable = value;
    }}

    IInputItem InputItem;
    IInputEnvanterItem InputEnvanterItem;
    IInputSlotMap InputSlotMap;
    public void Initialize()
    {
        InputItem = new CastRayClassForItem(OnTouchBegan,OnTouching,OnTouchEndItem);
        InputEnvanterItem = new CastRayClassForEnvanterItem(OnTouchBegan,OnTouching,OnTouchEndEnvanterItem);
        InputSlotMap = new CastRayClassForSlotMap();
    }
    public void UpdateTick(int itemlayerMask,int gridLayerMask,int envanterLayerMask)
    {   
        // item
        if(InputItem.UnSelectClickable())
        {
            DragAndDropable = null;
        }
        if (InputItem.CheckClickable(itemlayerMask))
        {
            DragAndDropable = InputItem.SelectClickable(itemlayerMask);
        }

        // envanterItem
        if(InputEnvanterItem.UnSelectClickable())
        {
            Envantable = null;
        }
        if(InputEnvanterItem.CheckClickable(envanterLayerMask))
        {
            Envantable = InputEnvanterItem.SelectClickable(envanterLayerMask);
        }

        // slotable
        Slotable = InputSlotMap.OnPointerSlotable(gridLayerMask);
        SlotForEnvanter = InputEnvanterItem.OnPointerSlotable(envanterLayerMask);
        InputItem.UpdateTick();
    }
    public void OnTouchBegan()
    {
        if(DragAndDropable != null)
            DragAndDropable.Select();
    }
    public void OnTouching()
    {
        if(DragAndDropable != null)
            DragAndDropable.OnDrag();
    }
    public void OnTouchEndEnvanterItem()
    {
        if(DragAndDropable != null)
        {
            DragAndDropable.OnTouchEnd();
            if(Slotable != null)
            {
                Slotable.OnPointerExit();
                Slotable = null;
            }
        }
    }
    public void OnTouchEndItem()
    {
        if(DragAndDropable != null)
        {
            DragAndDropable.OnTouchEnd();
            if(SlotForEnvanter != null)
            {
                SlotForEnvanter.OnPointerExit();
                SlotForEnvanter = null;
            }
        }
    }
    
}
