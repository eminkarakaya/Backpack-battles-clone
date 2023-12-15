using UnityEngine;
public class InputManager
{
    ISlotForGrid slotable;
    // ISlotForGrid Slotable{get=>slotable;
    // set{
    //     if(value == null && Envantable != null)
    //     {
    //         if(slotable != null)
    //         {
    //             // slotable.OnPointerExit();
    //             slotable = value;
    //         }
    //         return;
    //     }
    //     if(value != slotable)
    //     {       
    //         if(Envantable != null)
    //         {
    //             if(slotable != null)
    //                 // slotable.OnPointerExit();
    //             slotable = value;
    //             if(slotable != null)
    //             {
    //                 // slotable.OnPointerEnter();
    //             }
    //         }
    //         return;
    //     }
    //     slotable = value;
    // }}

    ISlotForEnvanter slotForEnvanter;
    ISlotForEnvanter SlotForEnvanter{get=>slotForEnvanter;
        set{
            if(value == null && DragAndDropable != null)
            {
                if(slotForEnvanter != null)
                {
                    slotForEnvanter.OnPointerExitWhileSelectedObject();
                    slotForEnvanter = value;
                }
                return;
            }
            if(value != slotForEnvanter)
            {       
                if(DragAndDropable != null)
                {
                    
                    if(slotForEnvanter != null)
                        slotForEnvanter.OnPointerExitWhileSelectedObject();
                    slotForEnvanter = value;
                    if(slotForEnvanter != null)
                    {
                        slotForEnvanter.OnPointerEnterWhileSelectedObject();
                    }
                }
                return;
            }
            slotForEnvanter = value;
        }}
        
    IDragAndDropable dragAndDropablePointerEnter;
    public IDragAndDropable DragAndDropablePointerEnter { get => dragAndDropablePointerEnter; set{
        if(value == null)
        {
            if(dragAndDropablePointerEnter != null)
            {
                dragAndDropablePointerEnter.OnPointerExit();    
            }
            dragAndDropablePointerEnter = null;
            return;
        }
        else if(value != dragAndDropablePointerEnter)
        {
            if(dragAndDropablePointerEnter != null)
            {
                dragAndDropablePointerEnter.OnPointerExit();    
            }
            dragAndDropablePointerEnter = value;
            dragAndDropablePointerEnter.OnPointerEnter();
            return;
        }
    }}
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
    IEnvantable envantablePointerEnter;
    IEnvantable EnvantablePointerEnter{get => envantablePointerEnter; set{
        if(value == null)
        {
            if(envantablePointerEnter != null)
            {
                envantablePointerEnter.OnPointerExit();    
            }
            envantablePointerEnter = null;
            return;
        }
        else if(value != envantablePointerEnter)
        {
            if(envantablePointerEnter != null)
            {
                envantablePointerEnter.OnPointerExit();    
            }
            envantablePointerEnter = value;
            envantablePointerEnter.OnPointerEnter();
            return;
        }
    }}
    IEnvantable envantable;
    IEnvantable Envantable{get=>envantable;
    set{
        envantable = value;
            if(envantable != null)
        envantable.Select();
    }}
    IInputItem InputItem;
    IInputEnvanterItem InputEnvanterItem;
    IInputSlotMap InputSlotMap;

    IEnvantable envantable1;
    public void Initialize()
    {
        InputItem = new CastRayClassForItem(OnTouchBegan,OnTouching,OnTouchEndItem,OnRotate);
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
        if(InputItem.CheckClickable(itemlayerMask,out IDragAndDropable outDragAndDropable))
        {
            DragAndDropablePointerEnter = outDragAndDropable;
            if (InputItem.CheckInput())
            {
                this.DragAndDropable = outDragAndDropable;
                DragAndDropable = InputItem.SelectClickable(itemlayerMask);
            }
            EnvantablePointerEnter = null;
        }
        else if(InputEnvanterItem.CheckClickable(envanterLayerMask,out envantable1))
        {
            EnvantablePointerEnter = envantable1;
            if(InputEnvanterItem.CheckInput())
            {
                if(!envantable1.IsInvolveAnyItem())
                {
                    this.Envantable = envantable1;
                    Envantable = InputEnvanterItem.SelectClickable(envanterLayerMask);
                } 
            }
        }
        if(outDragAndDropable == null)
        {
            DragAndDropablePointerEnter = null;
        }
        if(envantable1 == null)
        {
            EnvantablePointerEnter = null;
        }
        // envanterItem

        // slotable
        
        InputEnvanterItem.UpdateTick();
        // Slotable = InputSlotMap.OnPointerSlotable(gridLayerMask);
        SlotForEnvanter = InputEnvanterItem.OnPointerSlotable(envanterGridLayerMask);
        InputItem.UpdateTick();
        // OnRotate();
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
                SlotForEnvanter.OnPointerExitWhileSelectedObject();
                SlotForEnvanter = null;
            }
        }
    }
    public void OnRotate()
    {
        if(DragAndDropable != null)
        {
            DragAndDropable.RotateClockwise90Degree();
        }
        else if(Envantable != null)
        {
            Envantable.RotateClockwise90Degree();
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
        }
    }
    
}
