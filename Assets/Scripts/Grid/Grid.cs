using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour , ISlotForGrid
{
    public System.Action<Item> OnPuttingItem;
    [SerializeField] List<SubGridNeighbour> subGridNeighbours = new List<SubGridNeighbour>();
    // List<NeighbourTriggerableItemBase> neighbourBonuses = new List<NeighbourTriggerableItemBase>();
    [SerializeField] private GameObject neighbourTriggerObj;
    [SerializeField] private GameObject neighbourTriggerObjEmpty;
    public GridInEnvanter gridInEnvanter;
    public Grid right;
    public Grid left;
    public Grid up;
    public Grid down;
    public Vector2Int index;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    public void CheckTrigger(Item item)
    {
        // foreach (var bonus in neighbourBonuses)
        // {
        //     if(bonus.CheckExecutable(item))
        //     {
        //         OpenNeighbourTrigger();
        //         break;
        //     }
        // }
        // OpenNeighbourTriggerEmpty();
    }
    public void ExecuteSubNeighbours()
    {
        if(gridInEnvanter!=null && gridInEnvanter.gridInItem != null)
        {
            if(gridInEnvanter.gridInItem.item.TryGetComponent(out ItemMono item))
            {
                item.Execute();
            }
        }
    }
    public void AddSubgridNeighbour(SubGridNeighbour subGridNeighbour)
    {
        if(!subGridNeighbours.Contains(subGridNeighbour))
        {
            subGridNeighbours.Add(subGridNeighbour);
        }
    }
    public void RemoveSubgridNeighbour(SubGridNeighbour subGridNeighbour)
    {
        if(subGridNeighbours.Contains(subGridNeighbour))
        {
            subGridNeighbours.Remove(subGridNeighbour);
        }
    }
    private void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        FindNeightbours();
        // OnPuttingItem += OnPutting;
    }
    private void OnDisable() {
        // OnPuttingItem -= OnPutting;
    }
    public void OpenNeighbourTrigger()
    {
        neighbourTriggerObj.SetActive(true);
    }
    public void OpenNeighbourTriggerEmpty()
    {
        neighbourTriggerObjEmpty.SetActive(true);
    }
    public void CloseNeighbourTrigger()
    {
        neighbourTriggerObjEmpty.SetActive(false);
    }
    public void TriggerOnPointerEnter()
    {
        spriteRenderer.color = Color.green;
        // GridManager.Instance.selectedGrid = this;
    }
    public void TriggerOnPointerExit()
    {
        spriteRenderer.color = Color.white;
        // GridManager.Instance.selectedGrid = null;
    }
    public void OnPointerEnterWhileSelectedObject()
    {
        TriggerOnPointerEnter();
    }
    public void OnPointerExitWhileSelectedObject()
    {
        TriggerOnPointerExit();
    }
    public void FindNeightbours()
    {
        if(index.x == GridManager.Instance.scale.x-1)
        {
            right = null;
        }
        else
        {
            right = GridManager.Instance.GetGridByIndex(new Vector2Int(index.x + 1,index.y));
        }
        if(index.x == 0)
        {
            left = null;
        }
        else
        {
            left = GridManager.Instance.GetGridByIndex(new Vector2Int(index.x - 1,index.y));
        }
        if(index.y == 0)
        {
            up = null;
        }
        else
        {
            up = GridManager.Instance.GetGridByIndex(new Vector2Int(index.x ,index.y - 1));
        }
        if(index.y == GridManager.Instance.scale.y-1)
        {
            down = null;
        }
        else
        {
            down = GridManager.Instance.GetGridByIndex(new Vector2Int(index.x ,index.y + 1));
        }
    }
    
    public static Vector3 GetCenter(IEnumerable<Transform> grids)
    {
        Vector3 pos = Vector3.zero;
        int i = 0;
        foreach (var item in grids)
        {
            pos += item.position;
            i++;
        }
        return pos/i;
    }

    

}
