using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInEnvanter : MonoBehaviour, ISlotForEnvanter 
{
    public GridInItem gridInItem;
    public Envanter envanter;
    public Grid grid;
    SpriteRenderer spriteRenderer;
    private void Start() {
        envanter = GetComponentInParent<Envanter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OpenPutableColor()
    {
        spriteRenderer.color = Color.blue;
    }
    public void ClosePutableColor()
    {
        spriteRenderer.color = Color.gray;
    }
    
    public void OnPointerEnterWhileSelectedObject()
    {
        OpenPutableColor();
    }

    public void OnPointerExitWhileSelectedObject()
    {
        ClosePutableColor();
        TriggerOnPointerExit();
    }
    public bool CheckEnvanterGridAnyItem()
    {
        if(gridInItem == null)
        {
            return false;
        }
        if(!gridInItem.CheckEnvanterGridAnyItem())// item nullsa false, item null degılse true dondurur
        {
            return false;
        }
        return true;
    }
    public void SetItem(ItemDragAndDrop item)
    {
        if(gridInItem != null)
        {   
            gridInItem.itemDragAndDrop = item;
        }
    }
    /// Eger nullsa true doner.
    public bool IsNullItem()
    {
        if(gridInItem == null) 
            return true;
        return gridInItem.itemDragAndDrop == null;
    }
    public void TriggerOnPointerExit()
    {
        // spriteRenderer.color = Color.gray;
    }
    public ItemType GetItemType()
    {
        return gridInItem.itemDragAndDrop.GetComponent<ItemMono>().item.itemType;
    }
}
