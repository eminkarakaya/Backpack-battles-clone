using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Grid : MonoBehaviour , ISlotForGrid
{
    public Grid right;
    public Grid left;
    public Grid up;
    public Grid down;
    public Grid upRight;
    public Grid upLeft;
    public Grid downRight;
    public Grid downLeft;
    public Vector2Int index;
    SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        FindNeightbours();
    }
    public void TriggerOnPointerEnter()
    {
        spriteRenderer.color = Color.green;
        EnvanterSystem.Instance.selectedGrid = this;
    }
    public void TriggerOnPointerExit()
    {
        spriteRenderer.color = Color.white;
        EnvanterSystem.Instance.selectedGrid = null;
    }
    public void OnPointerEnter()
    {
        TriggerOnPointerEnter();
    }
    public void OnPointerExit()
    {
        TriggerOnPointerExit();
    }
    public void FindNeightbours()
    {
        if(index.x == EnvanterSystem.Instance.scale.x-1)
        {
            right = null;
        }
        else
        {
            right = EnvanterSystem.Instance.GetGridByIndex(new Vector2Int(index.x + 1,index.y));
        }
        if(index.x == 0)
        {
            left = null;
        }
        else
        {
            left = EnvanterSystem.Instance.GetGridByIndex(new Vector2Int(index.x - 1,index.y));
        }
        if(index.y == 0)
        {
            up = null;
        }
        else
        {
            up = EnvanterSystem.Instance.GetGridByIndex(new Vector2Int(index.x ,index.y - 1));
        }
    }
}
