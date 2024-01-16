using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInItem : MonoBehaviour,ISlotForItem
{
    public ItemDragAndDrop itemDragAndDrop;
    public GridInEnvanter gridInEnvanter;
    SpriteRenderer spriteRenderer;
    private void Start() {
        itemDragAndDrop = GetComponentInParent<ItemDragAndDrop>();
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
        spriteRenderer.color = Color.green;
    }
    public void OnPointerExitWhileSelectedObject()
    {
        TriggerOnPointerExit();
    }
    public bool CheckEnvanterGridAnyItem()
    {
        if(itemDragAndDrop == null)
        {
            return false;
        }
        return true;
    }
    public void SetItem(ItemDragAndDrop item)
    {
        this.itemDragAndDrop = item;
    }
    /// <summary>
    /// Eger nullsa true doner.
    /// </summary>
    /// <returns></returns>
    public bool IsNullItem()
    {
        return itemDragAndDrop == null;
    }
    public void TriggerOnPointerExit()
    {
        spriteRenderer.color = Color.gray;
    }
}
