using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IDragAndDropable ,ISlotable
{
    
    [SerializeField] protected GridInItem []  gridsInItemDefault;
    public GridInItem []  gridsInItemRotate;
    [SerializeField] private byte rotateStage;
    public byte RotateStage { get => rotateStage; set
    {
        rotateStage = value;
        rotateStage%=4;
    }} 

    [SerializeField] private GameObject outline;
    private bool isPlaced;
    public abstract List<GridInEnvanter> GetGrids(Vector3 pos);
    public abstract bool CheckGrid(Vector3 pos);
    public abstract GridInItem[] GetChildGridsInEnvanterByRotation(int rotationIndex);
    private void Start() {
        gridsInItemRotate = gridsInItemDefault ;
    }
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
    private void OpenPutableColorChilds()
    {
        foreach (var item in gridsInItemDefault)
        {
            item.OpenPutableColor();
        }
    }
    private void ClosePutableColorChilds()
    {
        foreach (var item in gridsInItemDefault)
        {
            item.ClosePutableColor();
        }
    }
    private void SetGridsColorToPuttingColor()
    {
        foreach (var item in gridsInItemDefault)
        {
            item.ClosePutableColor();
        }
    }
    public void OnDrag()
    {
        transform.position = MouseWorldPosition();
        if(EnvanterSystem.Instance.selectedGridInEnvanter !=null && CheckGrid(transform.position))
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
        if(EnvanterSystem.Instance.selectedGridInEnvanter != null)
        {
            if(CheckGrid(transform.position))
            {
                PutInSlot();
            }
        }
        outline.SetActive(false);
        EnvanterSystem.Instance.selectedItem = null;
    }

    public void Select()
    {
        outline.SetActive(true);
        if(isPlaced)
        {
            AssignGridInEnvantersGridsToNull();
        }
        isPlaced = false;
        EnvanterSystem.Instance.selectedItem = this;
    }
    private bool CheckAnyItem()
    {
        foreach (var item in gridsInItemDefault)
        {
            if (item.CheckEnvanterGridAnyItem())
                return true;
        }
        return false;
    }
    public void PutInSlot()
    {
        if(EnvanterSystem.Instance.selectedGridInEnvanter != null)
        {
            List<GridInEnvanter> grids = GetGrids(transform.position);
            List<ISlotable> slotables = CheckPlaceAnyEnvanterGrid(grids);

            foreach (var slotable in slotables)
            {
                slotable.TakeOffSlot();
            }
            AssignGridInEnvantersGrids(grids);
            transform.position = Grid.GetCenter(grids.Select(x=>x.transform));
            EnvanterSystem.Instance.selectedGridInEnvanter.TriggerOnPointerExit();
            isPlaced = true;
        }
    }
    private void AssignGridInEnvantersGrids(List<GridInEnvanter> grids)
    {
        for (int i = 0; i < gridsInItemRotate.Length; i++)
        {
            gridsInItemRotate[i].gridInEnvanter = grids[i];
            if(gridsInItemRotate[i].gridInEnvanter != null)
            {
                gridsInItemRotate[i].gridInEnvanter.SetItem(this);
            }
            grids[i].gridInItem = gridsInItemRotate[i];
        }
    }
    private List<ISlotable> CheckPlaceAnyEnvanterGrid(List<GridInEnvanter> grids)
    {
        List<ISlotable> envanters = new List<ISlotable>();
        foreach (var item in grids)
        {
            if(!item.IsNullItem())
            {
                if(!envanters.Contains(item.gridInItem.item))
                {
                    envanters.Add(item.gridInItem.item);
                }
            }
        }
        return envanters;
    }
    private void TakeOffPosition()
    {
        transform.position = Vector3.zero;
    }
    private void AssignGridInEnvantersGridsToNull()
    {
        for (int i = 0; i < gridsInItemDefault.Length; i++)
        {
            if(gridsInItemDefault[i].gridInEnvanter != null)
            {
                // gridsInItemDefault[i].gridInEnvanter.SetItem(null);
                gridsInItemDefault[i].gridInEnvanter.gridInItem = null;
                gridsInItemDefault[i].gridInEnvanter = null;
            }
            
        }
    }
   
    public void PuttingError()
    {
        
    }

    public void TakeOffSlot()
    {
        AssignGridInEnvantersGridsToNull();
        TakeOffPosition();
        isPlaced = false;
        
    }

    public void PutInSlotMap()
    {
        
    }

    public void TakeOffSlotMap()
    {
        
    }

    public void RotateCounterClockwise90Degree()
    {
        RotateStage --;
        gridsInItemRotate = GetChildGridsInEnvanterByRotation(rotateStage);
        RotateCounterClockwiseAnimation();
    }

    public void RotateClockwise90Degree()
    {
        RotateStage ++;
        gridsInItemRotate = GetChildGridsInEnvanterByRotation(rotateStage);
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

    public Grid[] GetGrids()
    {
        Grid [] grids = new Grid[gridsInItemRotate.Length];
        foreach (var item in gridsInItemRotate)
        {
            if(item.gridInEnvanter == null)
            {
                return null;
            }
        }
        return gridsInItemRotate.Select(x=>x.gridInEnvanter.grid).ToArray();
    }
}