using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGrid : MonoBehaviour
{
    public Grid selectedGrid;
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
            if(selectedGrid != null)
            {
                if(selectedGrid.gridInEnvanter != null)
                {
                    selectedGrid.gridInEnvanter.OnPointerExitWhileSelectedObject();
                }
                else
                    selectedGrid.OnPointerExitWhileSelectedObject();
                selectedGrid = null;
            }
            return null;
        }
        if(hit.collider.TryGetComponent(out Grid grid))
        {
            if(selectedGrid != null)
            {
                if(selectedGrid.gridInEnvanter != null)
                {
                    selectedGrid.gridInEnvanter.OnPointerExitWhileSelectedObject();
                }
                else
                {
                    selectedGrid.OnPointerExitWhileSelectedObject();
                }
            }
            else if(grid != selectedGrid)
            {
                if(selectedGrid != null)
                {
                    if(selectedGrid.gridInEnvanter != null)
                    {
                        selectedGrid.gridInEnvanter.OnPointerExitWhileSelectedObject();
                    }
                    else
                    {
                        selectedGrid.OnPointerExitWhileSelectedObject();
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
            selectedGrid = grid;
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
    public void AssignSelectedGridToNull()
    {
        selectedGrid = null;
    }
}
