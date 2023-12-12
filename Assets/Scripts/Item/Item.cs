using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragAndDropable ,ISlotable
{
    [SerializeField] private SubGrid [] subGrids;
    [SerializeField] private GameObject scaledGameObject;
    private float scaleMultiplier = 1.1f;
    [SerializeField] protected GridInItem []  gridsInItemDefault;
    [SerializeField] private byte rotateStage;
    [SerializeField] SpriteRenderer[] childSpriteRenderers;
    public byte RotateStage { get => rotateStage; set
    {
        rotateStage = value;
        rotateStage%=4;
    }} 

    // [SerializeField] private GameObject outline;
    private bool isPlaced;
    public List<GridInEnvanter> GetGridInEnvanters()
    {
        List<Grid> grids = new List<Grid>();
        foreach (var item in subGrids)
        {
            grids.Add(item.slot);
        }
        return grids.Select(x=>x.gridInEnvanter).ToList();
    }
    public bool CheckGrid()
    {
        foreach (var item in subGrids)
        {
            if(!item.CheckEnvanterGrid(InputManagerMono.Instance.layermaskGrid))
            {
                return false;
            }
        }
        return true;
    }
    private void Start() {
        subGrids =  GetComponentsInChildren<SubGrid>();
        childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
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
            PutInSlot();
        }
        SpriteOrderDefault();
        ScaleDefault();
        // outline.SetActive(false);
        GridManager.Instance.selectedItem = null;
    }

    public void Select()
    {
        // outline.SetActive(true);
        if(isPlaced)
        {
            AssignGridInEnvantersGridsToNull();
        }
        ScaleIncrease();
        SpriteOrderMax();
        isPlaced = false;
        GridManager.Instance.selectedItem = this;
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
        List<GridInEnvanter> grids = GetGridInEnvanters();
        List<ISlotable> slotables = CheckPlaceAnyEnvanterGrid(grids);

        foreach (var slotable in slotables)
        {
            slotable.TakeOffSlot();
        }
        AssignGridInEnvantersGrids(grids);
        transform.position = Grid.GetCenter(grids.Select(x=>x.transform));
        foreach (var item in gridsInItemDefault)
        {
            item.gridInEnvanter.ClosePutableColor();
        }
        // GridManager.Instance.selectedGridInEnvanter.TriggerOnPointerExit();
        isPlaced = true;
        ClosePutableColorChilds();
        
    }
    private void AssignGridInEnvantersGrids(List<GridInEnvanter> grids)
    {
        for (int i = 0; i < gridsInItemDefault.Length; i++)
        {
            gridsInItemDefault[i].gridInEnvanter = grids[i];
            if(gridsInItemDefault[i].gridInEnvanter != null)
            {
                gridsInItemDefault[i].gridInEnvanter.SetItem(this);
            }
            grids[i].gridInItem = gridsInItemDefault[i];
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

    public Grid[] GetGrids()
    {
        Grid [] grids = new Grid[gridsInItemDefault.Length];
        foreach (var item in gridsInItemDefault)
        {
            if(item.gridInEnvanter == null)
            {
                return null;
            }
        }
        return gridsInItemDefault.Select(x=>x.gridInEnvanter.grid).ToArray();
    }
    private void SpriteOrderMax()
    {
        for (int i = 0; i < childSpriteRenderers.Length; i++)
        {
            childSpriteRenderers[i].sortingLayerID = SortingLayer.NameToID(ItemManager.Instance.itemSelectedLayer);
        }
        
    }
    private void SpriteOrderDefault()
    {
        for (int i = 0; i < childSpriteRenderers.Length; i++)
        {
            childSpriteRenderers[i].sortingLayerID = SortingLayer.NameToID(ItemManager.Instance.itemDefaultLayer);
        }
    }
    private void ScaleIncrease()
    {
        scaledGameObject.transform.localScale = Vector3.one*scaleMultiplier;
    }
    private void ScaleDefault()
    {
        scaledGameObject.transform.localScale = Vector3.one;
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
}