using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGrid : MonoBehaviour
{
    public Grid slot;
    public bool CheckGrid(int layerMask)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        RaycastHit2D hit = Utils.CastRay(new Vector3(pos.x,pos.y,0),layerMask);
        if(hit.collider == null) return false;
        if(hit.collider.TryGetComponent(out ISlot grid))
        {
            return true;
        }
        return false;
    }
    public bool CheckEnvanterGrid(int layerMask)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        RaycastHit2D hit = Utils.CastRay(new Vector3(pos.x,pos.y,0),layerMask);
        if(hit.collider == null) return false;
        if(hit.collider.TryGetComponent(out Grid grid))
        {
            if(grid.gridInEnvanter!=null)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    public virtual Grid CastRay(int layerMask)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        RaycastHit2D hit = Utils.CastRay(new Vector3(pos.x,pos.y,0),layerMask);
        if(hit.collider == null) 
        {
            if(slot != null)
            {
                if(slot.gridInEnvanter != null)
                {
                    slot.gridInEnvanter.OnPointerExitWhileSelectedObject();
                }
                else
                    slot.OnPointerExitWhileSelectedObject();
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
                    slot.gridInEnvanter.OnPointerExitWhileSelectedObject();
                }
                else
                {
                    slot.OnPointerExitWhileSelectedObject();
                }
            }
            else if(grid != slot)
            {
                if(slot != null)
                {
                    if(slot.gridInEnvanter != null)
                    {
                        slot.gridInEnvanter.OnPointerExitWhileSelectedObject();
                    }
                    else
                    {
                        slot.OnPointerExitWhileSelectedObject();
                    }
                }
                if(grid != null)
                {
                    if(grid.gridInEnvanter != null)
                    {
                        grid.gridInEnvanter.OnPointerEnterWhileSelectedObject();
                    }
                    else
                    {
                        grid.OnPointerEnterWhileSelectedObject();
                    }
                }
            }
            slot = grid;
            if(grid.gridInEnvanter != null)
            {
                grid.gridInEnvanter.OnPointerEnterWhileSelectedObject();
            }
            else
                grid.OnPointerEnterWhileSelectedObject();
            return grid;
        }

        return null;
    }
}
