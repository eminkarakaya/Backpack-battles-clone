using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGridNeighbour : SubGrid
{
    public override Grid CastRay(int layerMask)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        RaycastHit2D hit = Utils.CastRay(new Vector3(pos.x,pos.y,0),layerMask);
        if(hit.collider == null) 
        {
            if(slot != null)
            {
                if(slot.gridInEnvanter != null)
                {
                    slot.CloseNeighbourTrigger();
                    // slot.gridInEnvanter.OnPointerExit();
                }
                else
                    slot.CloseNeighbourTrigger();
                slot = null;
            }
            return null;
        }
        if(hit.collider.TryGetComponent(out Grid grid))
        {
            if(slot != null)
            {
                if(slot.gridInEnvanter != null)
                {
                    slot.CloseNeighbourTrigger();
                }
                else
                {
                    slot.CloseNeighbourTrigger();
                    // slot.OnPointerExit();
                }
            }
            else if(grid != slot)
            {
                if(slot != null)
                {
                    if(slot.gridInEnvanter != null)
                    {
                        slot.CloseNeighbourTrigger();
                    }
                    else
                    {
                        slot.CloseNeighbourTrigger();
                        // slot.OnPointerExit();
                    }
                }
                if(grid != null)
                {
                    if(grid.gridInEnvanter != null)
                    {
                        grid.OpenNeighbourTrigger();
                    }
                    else
                    {
                        grid.OpenNeighbourTrigger();
                        // grid.OnPointerEnter();
                    }
                }
            }
            slot = grid;
            if(grid.gridInEnvanter != null)
            {
                slot.OpenNeighbourTrigger();
            }
            else
                slot.OpenNeighbourTrigger();
                // grid.OnPointerEnter();
            return grid;
        }

        return null;
    }
}
