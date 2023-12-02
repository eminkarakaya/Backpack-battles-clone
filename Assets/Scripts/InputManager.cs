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
    
    IDragAndDropable dragAndDropable;
    public IDragAndDropable DragAndDropable {
        get=>dragAndDropable;
        set
        {
            dragAndDropable = value;
            
            if(dragAndDropable != null)
                dragAndDropable.Select();
        }
    }
    IEnvantable envantable;

    IEnvantable Envantable{get=>envantable;
    set{
        envantable = value;
            if(envantable != null)
        envantable.Select();
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
        InputEnvanterItem = new CastRayClassForEnvanterItem(OnTouchBegan,OnTouchingEnvanterItem,OnTouchEndEnvanterItem);
        InputSlotMap = new CastRayClassForSlotMap();
    }
    public void UpdateTick(int itemlayerMask,int gridLayerMask,int envanterLayerMask,int envanterGridLayerMask)
    {   
        // item
        if(InputItem.UnSelectClickable())
        {
            DragAndDropable = null;
        }
        if(InputEnvanterItem.UnSelectClickable())
        {
            Envantable = null;
        }
        if (InputItem.CheckClickable(itemlayerMask))
        {
            DragAndDropable = InputItem.SelectClickable(itemlayerMask);
        }
        else if(InputEnvanterItem.CheckClickable(envanterLayerMask))
        {
            Envantable = InputEnvanterItem.SelectClickable(envanterLayerMask);
        }
        // envanterItem

        // slotable
        InputEnvanterItem.UpdateTick();
        Slotable = InputSlotMap.OnPointerSlotable(gridLayerMask);
        SlotForEnvanter = InputEnvanterItem.OnPointerSlotable(envanterGridLayerMask);
        InputItem.UpdateTick();
    }
    // item
    public void OnTouchBegan()
    {
        if(DragAndDropable != null)
        {
            DragAndDropable.Select();
        }

        else if(Envantable != null)
        {
            Envantable.Select();
        }
    }
    public void OnTouching()
    {
        if(DragAndDropable != null)
            DragAndDropable.OnDrag();
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

    // envanter
    // public void OnTouchBeganEnvanterItem()
    // {
        
    // }
    public void OnTouchingEnvanterItem()
    {
        if(Envantable != null)
            Envantable.OnDrag();
    }
    public void OnTouchEndEnvanterItem()
    {
        if(Envantable != null)
        {
            Envantable.OnTouchEnd();
            if(Slotable != null)
            {
                Slotable.OnPointerExit();
                Slotable = null;
            }
        }
    }
    
}
