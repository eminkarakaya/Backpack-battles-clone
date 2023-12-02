using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Envanter : MonoBehaviour, IEnvantable
{
    protected GridInEnvanter [] childGridsInEnvanterDefault; // sol ust -0  - sag ust -1  sol alt -2  sag alt -3
    GridInEnvanter [] childGridsInEnvanterByRotation;
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject outline;
    [SerializeField] private byte rotateStage;
    public byte RotateStage { get => rotateStage; set
    {
        rotateStage = value;
        rotateStage%=4;
    }} 

    bool isPlaced;
    [SerializeField] bool isSelected;
    public abstract List<Grid> GetGrids(Vector3 pos);
    public abstract bool CheckGrid(Vector3 pos);
    
    private void Start() {
        childGridsInEnvanterDefault = GetComponentsInChildren<GridInEnvanter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        childGridsInEnvanterByRotation = childGridsInEnvanterDefault;
    }

    public void OnDrag()
    {
        if(!isSelected) return;
        transform.position = MouseWorldPosition();
        if(EnvanterSystem.Instance.selectedGrid!=null && CheckGrid(transform.position))
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
            if(CheckGrid(transform.position))
            {
                PutInSlotMap();
            }
        }
        if(outline != null) 
            outline.SetActive(false);
        isSelected = false;
    }
    
    public void Select()
    {
        if(IsInvolveAnyItem())
        {
            return;
        }
        isSelected = true;
        if(isPlaced)
        {
            AssignGridInEnvantersGridsToNull();
        }
        isPlaced = false;
        if(outline != null) 
            outline.SetActive(true);
    }
    private bool CheckAnyItem()
    {
        foreach (var item in childGridsInEnvanterDefault)
        {
            if (item.CheckEnvanterGridAnyItem())
                return true;
        }
        return false;
    }
    public abstract GridInEnvanter[] GetChildGridsInEnvanterByRotation(int rotationIndex);
    
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    // buranın sırası
    private void AssignGridInEnvantersGrids(List<Grid> grids)
    {
        
        for (int i = 0; i < childGridsInEnvanterByRotation.Length; i++)
        {
            childGridsInEnvanterByRotation[i].grid = grids[i];
            grids[i].gridInEnvanter = childGridsInEnvanterByRotation[i];
        }
    }
    private void AssignGridInEnvantersGridsToNull()
    {
        for (int i = 0; i < childGridsInEnvanterDefault.Length; i++)
        {
            childGridsInEnvanterDefault[i].grid.gridInEnvanter = null;
            childGridsInEnvanterDefault[i].grid = null;
        }
    }
    private void SetGridsColorToPuttingColor()
    {
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.ClosePutableColor();
        }
    }
    private void OpenColliders()
    {
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.GetComponent<Collider2D>().enabled = true;
        }
    }
    private void CloseColliders()
    {
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OpenPutableColorChilds()
    {
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.OpenPutableColor();
        }
    }
    private void ClosePutableColorChilds()
    {
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.ClosePutableColor();
        }
    }
    public void PutInSlotMap()
    {
        if(EnvanterSystem.Instance.selectedGrid!= null)
        {
            // buranın sırası
            List<Grid> grids = GetGrids(transform.position);
            List<IEnvantable> envanters = CheckPlaceAnyEnvanter(grids);
            foreach (var item in envanters)
            {
                item.TakeOffSlotMap();
            }
            AssignGridInEnvantersGrids(grids);
            transform.position = Grid.GetCenter(grids.Select(x=>x.transform));
            EnvanterSystem.Instance.selectedGrid.TriggerOnPointerExit();
            OpenColliders();
            SetGridsColorToPuttingColor();
            isPlaced = true;
        }
    }
    private List<IEnvantable> CheckPlaceAnyEnvanter(List<Grid> grids)
    {
        List<IEnvantable> envanters = new List<IEnvantable>();
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
        TakeOffPosition();
    }
    public void RotateCounterClockwise90Degree()
    {
        RotateStage --;
        childGridsInEnvanterByRotation = GetChildGridsInEnvanterByRotation(rotateStage);
        RotateCounterClockwiseAnimation();
    }

    public void RotateClockwise90Degree()
    {
        RotateStage ++;
        childGridsInEnvanterByRotation = GetChildGridsInEnvanterByRotation(rotateStage);
        Rotate90ClockwiseAnimation();
    }
    private void Rotate90ClockwiseAnimation()
    {
        transform.rotation = Quaternion.Euler(0,0,RotateStage * 90);
    }
    private void RotateCounterClockwiseAnimation()
    {
        transform.rotation = Quaternion.Euler(0,0,RotateStage * 90);
    }

    public bool IsInvolveAnyItem()
    {
        return CheckAnyItem();
    }
}
