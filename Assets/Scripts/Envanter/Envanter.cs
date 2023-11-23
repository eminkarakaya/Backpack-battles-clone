using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter : MonoBehaviour, IDragAndDropable,IEnvantable
{
    GridInEnvanter [] childGridsInEnvanter; // sol ust -0  - sag ust -1  sol alt -2  sag alt -3
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject outline;
    public Vector2 EndPos { get; set; }
    bool isPlaced;

    private void Start() {
        childGridsInEnvanter = GetComponentsInChildren<GridInEnvanter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnDrag()
    {
        transform.position = MouseWorldPosition();
        if(EnvanterSystem.Instance.selectedGrid!=null&&EnvanterSystem.Instance.selectedGrid.CheckGrid(transform.position))
        {
            OpenPutableColorChilds();
        }
        else
        {
            ClosePutableColorChilds();
        }
    }
    public void OnTouchEnd()
    {
        if(EnvanterSystem.Instance.selectedGrid!=null) 
        {
            if(EnvanterSystem.Instance.selectedGrid.CheckGrid(transform.position))
            {
                PutInSlotMap();
            }
        }
        if(outline != null) 
            outline.SetActive(false);
    }

    public void Select()
    {
        if(isPlaced)
        {
            AssignGridInEnvantersGridsToNull();
        }
        isPlaced = false;
        if(outline != null) 
            outline.SetActive(true);
    }
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
    private void AssignGridInEnvantersGrids(List<Grid> grids)
    {
        for (int i = 0; i < childGridsInEnvanter.Length; i++)
        {
            childGridsInEnvanter[i].grid = grids[i];
            grids[i].gridInEnvanter = childGridsInEnvanter[i];
        }
    }
    private void AssignGridInEnvantersGridsToNull()
    {
        for (int i = 0; i < childGridsInEnvanter.Length; i++)
        {
            childGridsInEnvanter[i].grid.gridInEnvanter = null;
            childGridsInEnvanter[i].grid = null;
        }
    }
    
    private void SetGridsColorToPuttingColor()
    {
        foreach (var item in childGridsInEnvanter)
        {
            item.ClosePutableColor();
        }
    }
    private void OpenColliders()
    {
        foreach (var item in childGridsInEnvanter)
        {
            item.GetComponent<Collider2D>().enabled = true;
        }
    }
    private void CloseColliders()
    {
        foreach (var item in childGridsInEnvanter)
        {
            item.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OpenPutableColorChilds()
    {
        foreach (var item in childGridsInEnvanter)
        {
            item.OpenPutableColor();
        }
    }
    private void ClosePutableColorChilds()
    {
        foreach (var item in childGridsInEnvanter)
        {
            item.ClosePutableColor();
        }
    }
    public void PutInSlotMap()
    {
        if(EnvanterSystem.Instance.selectedGrid!= null)
        {
            List<Grid> grids = EnvanterSystem.Instance.selectedGrid.GetGrids(transform.position);
            List<Envanter> envanters = CheckPlaceAnyEnvanter(grids);
            foreach (var item in envanters)
            {
                item.TakeOffSlotMap();
            }
            AssignGridInEnvantersGrids(grids);
            transform.position = Grid.GetCenter(grids);
            EnvanterSystem.Instance.selectedGrid.TriggerOnPointerExit();
            OpenColliders();
            SetGridsColorToPuttingColor();
            isPlaced = true;
        }
    }
    private List<Envanter> CheckPlaceAnyEnvanter(List<Grid> grids)
    {
        List<Envanter> envanters = new List<Envanter>();
        foreach (var item in grids)
        {   
            if(item.gridInEnvanter != null)
            {
                if(!envanters.Contains(item.gridInEnvanter.envanter))
                {
                    envanters.Add(item.gridInEnvanter.envanter);
                }
            }
        }
        return envanters;
    }    
    private void TakeOffPosition()
    {
        transform.position = Vector3.zero;
    }
    public void TakeOffSlotMap()
    {        
        AssignGridInEnvantersGridsToNull();
        TakeOffPosition();
        isPlaced = false;
        CloseColliders();
    }

    public void PuttingError()
    {
        
    }
}
