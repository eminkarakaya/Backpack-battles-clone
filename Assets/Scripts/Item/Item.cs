using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragAndDropable ,ISlotable
{
    public List<GridInItem> gridsInItem;
    public byte RotateStage { get; set; }

    [SerializeField] private GameObject outline;
    private bool isPlaced;

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    public void OnDrag()
    {
        transform.position = MouseWorldPosition();
    }

    public void OnTouchEnd()
    {
        if(EnvanterSystem.Instance.selectedGridInEnvanter != null)
        {
            if(EnvanterSystem.Instance.selectedGridInEnvanter.CheckEnvanterGrid(transform.position))
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
        foreach (var item in gridsInItem)
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
            List<GridInEnvanter> grids = EnvanterSystem.Instance.selectedGridInEnvanter.GetGridsUpDown(transform.position);
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
        for (int i = 0; i < gridsInItem.Count; i++)
        {
            gridsInItem[i].gridInEnvanter = grids[i];
            gridsInItem[i].gridInEnvanter.SetItem(this);
            grids[i].slotForItem = gridsInItem[i];
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
        for (int i = 0; i < gridsInItem.Count; i++)
        {
            gridsInItem[i].gridInEnvanter.SetItem(null);
            gridsInItem[i].gridInEnvanter.slotForItem = null;
            gridsInItem[i].gridInEnvanter = null;
            
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
}