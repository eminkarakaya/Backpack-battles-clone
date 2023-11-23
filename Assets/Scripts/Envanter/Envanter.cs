using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter : MonoBehaviour, IDragAndDropable,IEnvantable
{
    GridInEnvanter [] childGridsInEnvanter; // sol ust -0  - sag ust -1  sol alt -2  sag alt -3
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject outline;
    [SerializeField] private Direction4 direction;
    public Vector2 EndPos { get; set; }

    private void Start() {
        childGridsInEnvanter = GetComponentsInChildren<GridInEnvanter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
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
    private void AssignGridInEnvantersGrids(List<Grid> grids)
    {
        for (int i = 0; i < childGridsInEnvanter.Length; i++)
        {
            childGridsInEnvanter[i].grid = grids[i];
        }
    }
    private void SetGridsColorToPuttingColor()
    {
        foreach (var item in childGridsInEnvanter)
        {
            item.ClosePutableColor();
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
        if(outline != null) 
            outline.SetActive(true);
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
            AssignGridInEnvantersGrids(grids);
            transform.position = Grid.GetCenter(grids);
            // transform.position = EnvanterSystem.Instance.selectedGrid.transform.position;
            EnvanterSystem.Instance.selectedGrid.TriggerOnPointerExit();
            OpenColliders();
            SetGridsColorToPuttingColor();
        }
    }
    

    public void TakeItOutSlotMap()
    {
        
    }

    public void PuttingError()
    {
        
    }
}
