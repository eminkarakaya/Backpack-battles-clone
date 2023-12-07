using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInEnvanter : MonoBehaviour, ISlotForEnvanter
{
    public GridInItem gridInItem;
    // public Item item;
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
    
    public void OnPointerEnter()
    {
        spriteRenderer.color = Color.green;
        EnvanterSystem.Instance.selectedGridInEnvanter = this;
    }

    public void OnPointerExit()
    {
        TriggerOnPointerExit();
    }
    public bool CheckEnvanterGrid3x1(Vector3 pos, byte rotateStage)
    {
        return grid.CheckEnvanterGrid3x1(pos,rotateStage);
    }
    public bool CheckEnvanterGrid2x1(Vector3 pos, byte rotateStage)
    {
        return grid.CheckEnvanterGrid2x1(pos,rotateStage);
    }
    public List<GridInEnvanter> GetGrids2x1(Vector3 pos,byte rotateStage)
    {
        return grid.GetEnvanterGrids2x1(pos,rotateStage);
    }
    public List<GridInEnvanter> GetGrids3x1(Vector3 pos,byte rotateStage)
    {
        return grid.GetEnvanterGrids3x1(pos,rotateStage);
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
        // if(item == null)
        // {
        //     return false;
        // }
        // return true;
    }
    public void SetItem(Item item)
    {
        if(gridInItem != null)
        {   
            gridInItem.item = item;
        }
    }
    /// <summary>
    /// Eger nullsa true doner.
    /// </summary>
    /// <returns></returns>
    public bool IsNullItem()
    {
        if(gridInItem == null) 
            return true;
        return gridInItem.item == null;
    }
    public void TriggerOnPointerExit()
    {
        spriteRenderer.color = Color.gray;
        EnvanterSystem.Instance.selectedGridInEnvanter = null;
    }
    
}
