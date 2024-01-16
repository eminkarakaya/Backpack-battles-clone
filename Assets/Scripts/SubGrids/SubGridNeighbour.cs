using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon, Food
}
public class SubGridNeighbour : SubGrid
{
    [SerializeField] ItemType itemType;
    public void OpenNeighbourTrigger(Grid grid)
    {
        if (grid.gridInEnvanter != null)
        {

            if (grid.gridInEnvanter.gridInItem != null)
            {
                if (itemType == grid.gridInEnvanter.gridInItem.itemDragAndDrop.GetComponent<ItemMono>().item.itemType)
                {
                    grid.OpenNeighbourTrigger();
                }
            }
            else
            {
                grid.OpenNeighbourTriggerEmpty();
            }
        }
        else
        {
            grid.OpenNeighbourTriggerEmpty();
            // grid.OnPointerEnter();
        }
    }
    public void CloseNeighbourTrigger()
    {
        if (selectedGrid.gridInEnvanter != null)
        {
            selectedGrid.CloseNeighbourTrigger();
            // slot.gridInEnvanter.OnPointerExit();
        }
        else
            selectedGrid.CloseNeighbourTrigger();
    }
    public override Grid CastRay(int layerMask)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        RaycastHit2D hit = Utils.CastRay(new Vector3(pos.x, pos.y, 0), layerMask);
        if (hit.collider == null)
        {
            if (selectedGrid != null)
            {
                CloseNeighbourTrigger();
                selectedGrid = null;
            }
            return null;
        }
        if (hit.collider.TryGetComponent(out Grid grid))
        {
            if (selectedGrid != null)
            {
                CloseNeighbourTrigger();
            }
            else if (grid != selectedGrid)
            {
                if (selectedGrid != null)
                {
                    CloseNeighbourTrigger();
                }
                if (grid != null)
                {
                    OpenNeighbourTrigger(grid);
                }
            }
            selectedGrid = grid;
            OpenNeighbourTrigger(grid);
            return grid;
        }

        return null;
    }
}
