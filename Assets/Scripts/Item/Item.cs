using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragAndDropable ,ISlotable
{
    
    [SerializeField] private GridInItem []  gridsInItemDefault;
    public GridInItem []  gridsInItemRotate;
    [SerializeField] private byte rotateStage;
    public byte RotateStage { get => rotateStage; set
    {
        rotateStage = value;
        rotateStage%=4;
    }} 

    [SerializeField] private GameObject outline;
    private bool isPlaced;
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
        if(EnvanterSystem.Instance.selectedGridInEnvanter!=null && EnvanterSystem.Instance.selectedGridInEnvanter.CheckEnvanterGrid(transform.position,RotateStage))
        {
            OpenPutableColorChilds();
        }
        else
        {
            ClosePutableColorChilds();
        }
        // transform.position = MouseWorldPosition();
    }

    public void OnTouchEnd()
    {
        if(EnvanterSystem.Instance.selectedGridInEnvanter != null)
        {
            if(EnvanterSystem.Instance.selectedGridInEnvanter.CheckEnvanterGrid(transform.position,RotateStage))
            {
                PutInSlot();
            }

        }
        outline.SetActive(false);
        EnvanterSystem.Instance.selectedItem = null;
    }

    public void Select()
    {
        // if(CheckAnyItem())
        // {

        // }
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
            List<GridInEnvanter> grids = EnvanterSystem.Instance.selectedGridInEnvanter.GetGridsUpDown(transform.position,RotateStage);
            List<ISlotable> envanters = CheckPlaceAnyEnvanterGrid(grids);

            foreach (var item in envanters)
            {
                item.TakeOffSlot();
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
            gridsInItemRotate[i].gridInEnvanter.SetItem(this);
            grids[i].slotForItem = gridsInItemRotate[i];
        }
    }
    private List<ISlotable> CheckPlaceAnyEnvanterGrid(List<GridInEnvanter> grids)
    {
        List<ISlotable> envanters = new List<ISlotable>();
        foreach (var item in grids)
        {
            if(!item.IsNullItem())
            {
                if(!envanters.Contains(item.item))
                {
                    envanters.Add(item.item);
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
            gridsInItemDefault[i].gridInEnvanter.SetItem(null);
            gridsInItemDefault[i].gridInEnvanter.slotForItem = null;
            gridsInItemDefault[i].gridInEnvanter = null;
            
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
    public GridInItem []  GetChildGridsInEnvanterByRotation(int rotationIndex)
    {
        GridInItem []  temp = new GridInItem [gridsInItemDefault.Length];

        if(rotationIndex == 0) // default
        {
            return gridsInItemDefault;
        }
        else if(rotationIndex == 1) // 90 clockwise
        {
            return gridsInItemDefault;
        }
        else if(rotationIndex == 2) // 180
        {
            temp[0] = gridsInItemDefault[1];
            temp[1] = gridsInItemDefault[0];
            return temp;

        }
        else // 270 clockwise
        {
            temp[0] = gridsInItemDefault[1];
            temp[1] = gridsInItemDefault[0];
            return temp;
        }
    }
}