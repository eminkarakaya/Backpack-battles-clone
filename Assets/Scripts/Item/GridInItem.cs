using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInItem : MonoBehaviour,ISlotForItem
{
    public Item item;
    public GridInEnvanter gridInEnvanter;
    SpriteRenderer spriteRenderer;
    private void Start() {
        item = GetComponentInParent<Item>();
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
    
    public void OnPointerEnter()
    {
        spriteRenderer.color = Color.green;
        EnvanterSystem.Instance.selectedGridInItem = this;
    }
    public void OnPointerExit()
    {
        TriggerOnPointerExit();
    }
    public bool CheckEnvanterGridAnyItem()
    {
        if(item == null)
        {
            return false;
        }
        return true;
    }
    public void SetItem(Item item)
    {
        this.item = item;
    }
    /// <summary>
    /// Eger nullsa true doner.
    /// </summary>
    /// <returns></returns>
    public bool IsNullItem()
    {
        return item == null;
    }
    public void TriggerOnPointerExit()
    {
        spriteRenderer.color = Color.gray;
        EnvanterSystem.Instance.selectedGridInItem = null;
    }
}
