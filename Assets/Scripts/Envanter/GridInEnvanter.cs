using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInEnvanter : MonoBehaviour, ISlotForEnvanter
{
    public GridInItem slotForItem;
    public ISlotable item{get;private set;}
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

    public bool CheckEnvanterGrid(Vector3 pos, byte rotateStage)
    {
        return grid.CheckEnvanterGrid(pos,rotateStage);
    }
    public List<GridInEnvanter> GetGridsUpDown(Vector3 pos,byte rotateStage)
    {
        return grid.GetGridsUpDown(pos,rotateStage);
    }
    public bool CheckEnvanterGridAnyItem()
    {
        if(item == null)
        {
            return false;
        }
        return true;
    }
    public void SetItem(ISlotable item)
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
        EnvanterSystem.Instance.selectedGridInEnvanter = null;
    }
}
