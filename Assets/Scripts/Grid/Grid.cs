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
    BoxCollider2D boxCollider2D;
    private void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();
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
        if(index.y == EnvanterSystem.Instance.scale.y-1)
        {
            down = null;
        }
        else
        {
            down = EnvanterSystem.Instance.GetGridByIndex(new Vector2Int(index.x ,index.y + 1));
        }
    }
    public Direction4 GetDirection(Vector3 pos)
    {
        if(pos.x > boxCollider2D.bounds.center.x && pos.y > boxCollider2D.bounds.center.y)
        {
            return Direction4.UpRight;
        }
        else if(pos.x <= boxCollider2D.bounds.center.x && pos.y > boxCollider2D.bounds.center.y)
        {
            return Direction4.UpLeft;
        }
        else if(pos.x <= boxCollider2D.bounds.center.x && pos.y <= boxCollider2D.bounds.center.y)
        {
            return Direction4.DownLeft;
        }
        else if(pos.x > boxCollider2D.bounds.center.x && pos.y <= boxCollider2D.bounds.center.y)
        {
            return Direction4.DownRight;
        }
        return Direction4.DownRight;
    }
    /// <summary>
    /// 1- bool lazım uygun mu dıye
    /// 
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    // public List<Grid> GetGridsByDirection(Direction dir)
    // {

    // }
    public bool CheckGrid(Vector3 pos)
    {
        Direction4 dir = GetDirection(pos);
        if(dir == Direction4.UpLeft)
        {
            if(up != null && left != null && left.up != null)
                return true;
            return false;
        }
        else if(dir == Direction4.UpRight)
        {
            if(up != null && right != null && right.up != null)
                return true;
            return false;
            
        }
        else if(dir == Direction4.DownLeft)
        {
            if(down != null && left != null && left.down != null)
                return true;
            return false;
            
        }
        else if(dir == Direction4.DownRight)
        {
            if(down != null && right != null && right.down != null)
                return true;
            return false;
        }
        return false;
    }
    public List<Grid> GetGrids(Vector3 pos)
    {
        Direction4 dir = GetDirection(pos);
        List<Grid> grids = new List<Grid>();
        if(dir == Direction4.UpLeft)
        {
            if(up != null && left != null && left.up != null)
            {
                // ekleme sırası önemli
                grids.Add(left.up);
                grids.Add(up);
                grids.Add(left);
                grids.Add(this);
                return grids;
            }
            return null;
        }
        else if(dir == Direction4.UpRight)
        {
            if(up != null && right != null && right.up != null)
            {
                grids.Add(up);
                grids.Add(right.up);
                grids.Add(this);
                grids.Add(right);
                return grids;
            }
            return null;
            
        }
        else if(dir == Direction4.DownLeft)
        {
            if(down != null && left != null && left.down != null)
            {
                grids.Add(left);
                grids.Add(this);
                grids.Add(left.down);
                grids.Add(down);
                return grids;
            }
            return null;
            
        }
        else if(dir == Direction4.DownRight)
        {
            if(down != null && right != null && right.down != null)
            {
                grids.Add(this);
                grids.Add(right);
                grids.Add(down);
                grids.Add(right.down);
                return grids;
            }
            return null;
            
        }
        return null;
    }
    public static Vector3 GetCenter(List<Grid> grids)
    {
        Vector3 pos = Vector3.zero;
        foreach (var item in grids)
        {
            pos += item.transform.position;
        }
        return pos/grids.Count;
    }
}
