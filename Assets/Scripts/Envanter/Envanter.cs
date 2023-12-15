using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Envanter : MonoBehaviour, IEnvantable
{
    [SerializeField] private SubGridPutting [] subGrids;
    protected GridInEnvanter [] childGridsInEnvanterDefault; // sol ust -0  - sag ust -1  sol alt -2  sag alt -3
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject outline;
    [SerializeField] private byte rotateStage;
    public byte RotateStage { get => rotateStage; set
    {
        rotateStage = value;
        rotateStage%=4;
    }} 

    [SerializeField] bool isPlaced;
    private void Start() {
        subGrids = GetComponentsInChildren<SubGridPutting>();
        childGridsInEnvanterDefault = GetComponentsInChildren<GridInEnvanter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnDrag()
    {
        transform.position = MouseWorldPosition();
        foreach (var item in subGrids)
        {
            item.CastRay(InputManagerMono.Instance.layermaskGrid);
        }
    }
    public void OnTouchEnd()
    {
        
        if(CheckGrid())
        {
            PutInSlotMap();
        }
    }
    
    public void Select()
    {
        if(IsInvolveAnyItem())
        {
            return;
        }
        if(isPlaced)
        {
            AssignGridInEnvantersGridsToNull();
        }
        isPlaced = false;
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
    
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    // buranın sırası
    private void AssignGridInEnvantersGrids(List<Grid> grids) // koyulacak grıdler
    {
        for (int i = 0; i < childGridsInEnvanterDefault.Length; i++)
        {
            
            childGridsInEnvanterDefault[i].grid = grids[i];
            
            grids[i].gridInEnvanter = childGridsInEnvanterDefault[i];
        }
    }
    private ISlotable [] AssignGridInEnvantersGridsToNull()
    {
        List<ISlotable> slotables = new List<ISlotable>(); // grid
        for (int i = 0; i < childGridsInEnvanterDefault.Length; i++)
        {
            if(childGridsInEnvanterDefault[i].grid.gridInEnvanter.gridInItem != null &&  childGridsInEnvanterDefault[i].grid.gridInEnvanter.gridInItem.item != null)
            {
                slotables.Add(childGridsInEnvanterDefault[i].grid.gridInEnvanter.gridInItem.item);
            }
            if(childGridsInEnvanterDefault[i].gridInItem != null && childGridsInEnvanterDefault[i].gridInItem.gridInEnvanter != null)
            {
                // childGridsInEnvanterDefault[i].SetItem(null);
                childGridsInEnvanterDefault[i].gridInItem.gridInEnvanter = null;
            }
            childGridsInEnvanterDefault[i].gridInItem = null;
            childGridsInEnvanterDefault[i].grid.gridInEnvanter = null;
            childGridsInEnvanterDefault[i].grid = null;

        }
        return slotables.ToArray();
    }

    
    private void SetGridsColorToPuttingColor()
    {
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.ClosePutableColor();
        }
    }
    // private void OpenColliders()
    // {
    //     foreach (var item in childGridsInEnvanterDefault)
    //     {
    //         item.GetComponent<Collider2D>().enabled = true;
    //     }
    // }
    // private void CloseColliders()
    // {
    //     foreach (var item in childGridsInEnvanterDefault)
    //     {
    //         item.GetComponent<Collider2D>().enabled = false;
    //     }
    // }
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
    private bool CheckGrid()
    {
        foreach (var item in subGrids)
        {
            if(!item.CheckGrid(InputManagerMono.Instance.layermaskGrid))
            {
                return false;
            }
        }
        return true;
    }
    private List<Grid> GetGrids()
    {   
        List<Grid> grids = new List<Grid>();
        foreach (var item in subGrids)
        {
            grids.Add(item.slot);
        }
        return grids;
    }
    public void PutInSlotMap()
    {
        // koyulacak grıdler sırayla
        List<Grid> grids = GetGrids();


        // cıkarılacak envanterler
        List<IEnvantable> envanters = CheckPlaceAnyEnvanter(grids);

        List<ISlotable> slotables = new List<ISlotable>();
        List<Grid> gridsEnvanter = new List<Grid>();
        for (int i = 0; i < envanters.Count; i++)
        {
            
            // cıkarılan envanterlerın bulundugu grıdlerın 
         



            for (int j = 0; j < grids.Count; j++)
            {
                if(grids[j].gridInEnvanter != null && !grids[j].gridInEnvanter.IsNullItem())
                {
                    gridsEnvanter.Add(grids[j]);
                    // itemi olan gridleri alıyoruz
                }
                if(grids[j].gridInEnvanter != null && grids[j].gridInEnvanter.gridInItem != null)
                {
                    childGridsInEnvanterDefault[j].gridInItem = grids[j].gridInEnvanter.gridInItem;
                    childGridsInEnvanterDefault[j].gridInItem.gridInEnvanter = childGridsInEnvanterDefault[j];
                    grids[j].gridInEnvanter.gridInItem = null;
                }
            }
            List<ISlotable> slotables1 = slotables1 = envanters[i].TakeOffSlotMap().ToList();
            for (int j = 0; j < slotables1.Count; j++)
            {
                slotables.Add(slotables1[j]);
            }
            // itemi olan gridlerdeki itemleri alıyoruz.
        }
        AssignGridInEnvantersGrids(grids);
        transform.position = Grid.GetCenter(grids.Select(x=>x.transform));
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.grid.TriggerOnPointerExit();
        }
        // GridManager.Instance.selectedGrid.TriggerOnPointerExit();
        // OpenColliders();
        SetGridsColorToPuttingColor();
        isPlaced = true;
        for (int i = 0; i < slotables.Count; i++)
        {
            if(slotables[i].GetGrids() == null)
            {
                slotables[i].TakeOffSlot();
                
            }
            else
            {
                foreach (var item in slotables[i].GetGrids())
                {
                    if(item == null || item.gridInEnvanter == null)
                    {
                        slotables[i].TakeOffSlot();
                    }
                }
            }
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
    public ISlotable[] TakeOffSlotMap()
    {        
        foreach (var item in childGridsInEnvanterDefault)
        {
            item.ClosePutableColor();
        }
        TakeOffPosition();
        isPlaced = false;
        // CloseColliders();
        return AssignGridInEnvantersGridsToNull();
    }

    public void PuttingError()
    {
        TakeOffPosition();
    }
    public void RotateCounterClockwise90Degree()
    {
        RotateStage --;
        RotateCounterClockwiseAnimation();
    }

    public void RotateClockwise90Degree()
    {
        RotateStage ++;
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

    public void OnPointerEnter()
    {
        outline.SetActive(true);
    }

    public void OnPointerExit()
    {
        outline.SetActive(false);
        
    }
}
