using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Grid : MonoBehaviour , ISlotForGrid
{
    [SerializeField] private GameObject neighbourTriggerObj;
    public GridInEnvanter gridInEnvanter;
    public Grid right;
    public Grid left;
    public Grid up;
    public Grid down;
    public Vector2Int index;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    private void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        FindNeightbours();
    }
    public void OpenNeighbourTrigger()
    {
        neighbourTriggerObj.SetActive(true);
    }
    public void CloseNeighbourTrigger()
    {
        neighbourTriggerObj.SetActive(false);
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
    public DirectionUpDownRightLeft GetDirectionUpDown(Vector3 pos,byte rotateIndex)
    {
        if(rotateIndex == 0)
        {
            if(pos.y > boxCollider2D.bounds.center.y)
            {
                return DirectionUpDownRightLeft.Up;
            }
            else
            {
                return DirectionUpDownRightLeft.Down;
            }
        }
        else if(rotateIndex == 1)
        {
            if(pos.x > boxCollider2D.bounds.center.x)
            {
                return DirectionUpDownRightLeft.Right;
            }
            else
            {
                return DirectionUpDownRightLeft.Left;
            }

        }
        else if(rotateIndex == 2)
        {

            if(pos.y > boxCollider2D.bounds.center.y)
            {
                return DirectionUpDownRightLeft.Up;
            }
            else
            {
                return DirectionUpDownRightLeft.Down;
            }
        }
        else
        {
            if(pos.x > boxCollider2D.bounds.center.x)
            {
                return DirectionUpDownRightLeft.Right;
            }
            else
            {
                return DirectionUpDownRightLeft.Left;
            }
        }
    }
    public Direction4 GetDirection4(Vector3 pos)
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
    public bool CheckEnvanterGrid2x1(Vector3 pos,byte rotateIndex)
    {
        DirectionUpDownRightLeft dir = GetDirectionUpDown(pos,rotateIndex);
        if(rotateIndex == 0)
        {
            if(dir == DirectionUpDownRightLeft.Up)
            {
                if( up.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            else if(dir == DirectionUpDownRightLeft.Down)
            {
                if(down.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        else if(rotateIndex == 1)
        {
            if(dir == DirectionUpDownRightLeft.Right)
            {
                if(right != null && right.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            else if(dir == DirectionUpDownRightLeft.Left)
            {
                if(left != null && left.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        } 
        else if(rotateIndex == 2)
        {
            if(dir == DirectionUpDownRightLeft.Up)
            {
                if(up.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            else if(dir == DirectionUpDownRightLeft.Down)
            {
                if(down.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        else 
        {
            if(dir == DirectionUpDownRightLeft.Right)
            {
                if(right != null && right.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            else if(dir == DirectionUpDownRightLeft.Left)
            {
                if(left != null && left.gridInEnvanter != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        
    }
    public bool CheckEnvanterGrid3x1(Vector3 pos,byte rotateIndex)
    {
        if(rotateIndex == 0)
        {
            if(right == null || right.gridInEnvanter == null || left == null || left.gridInEnvanter == null)
            {
                return false;
            }
            return true;
        }
        else if(rotateIndex == 1)
        {
            if(up == null || up.gridInEnvanter == null || down == null || down.gridInEnvanter == null)
            {
                return false;
            }
            return true;
        } 
        else if(rotateIndex == 2)
        {
            if(right == null || right.gridInEnvanter == null || left == null || left.gridInEnvanter == null)
            {
                return false;
            }
            return true;
        }
        else 
        {
            if(up == null || up.gridInEnvanter == null || down == null || down.gridInEnvanter == null)
            {
                return false;
            }
            return true;
        }
        
    }
    public List<GridInEnvanter> GetEnvanterGrids2x1(Vector3 pos,byte rotateIndex)
    {
        DirectionUpDownRightLeft dir = GetDirectionUpDown(pos,rotateIndex);
        List<GridInEnvanter> grids = new List<GridInEnvanter>();
        if(rotateIndex == 0)
        {
            if(dir == DirectionUpDownRightLeft.Up)
            {
                if(up != null && up.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(up.gridInEnvanter);
                    grids.Add(this.gridInEnvanter);
                }
            }
            else if(dir == DirectionUpDownRightLeft.Down)
            {
                if(down != null && down.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(this.gridInEnvanter);
                    grids.Add(down.gridInEnvanter);
                }
            }
        }
        else if(rotateIndex == 1)
        {
            if(dir == DirectionUpDownRightLeft.Left)
            {
                if(left != null && left.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(left.gridInEnvanter);
                    grids.Add(this.gridInEnvanter);
                }
            }
            else if(dir == DirectionUpDownRightLeft.Right)
            {
                if(right != null && right.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(this.gridInEnvanter);
                    grids.Add(right.gridInEnvanter);
                }
            }
        }
        else if(rotateIndex == 2)
        {
            if(dir == DirectionUpDownRightLeft.Up)
            {
                if(up != null && up.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(up.gridInEnvanter);
                    grids.Add(this.gridInEnvanter);
                }
            }
            else if(dir == DirectionUpDownRightLeft.Down)
            {
                if(down != null && down.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(this.gridInEnvanter);
                    grids.Add(down.gridInEnvanter);
                }
            }
        }
        else
        {
            if(dir == DirectionUpDownRightLeft.Left)
            {
                if(left != null && left.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(left.gridInEnvanter);
                    grids.Add(this.gridInEnvanter);
                }
            }
            else if(dir == DirectionUpDownRightLeft.Right)
            {
                if(right != null && right.gridInEnvanter != null)
                {
                    // ekleme sırası önemli
                    grids.Add(this.gridInEnvanter);
                    grids.Add(right.gridInEnvanter);
                }
            }
        }
        return grids;
    }

    public List<GridInEnvanter> GetEnvanterGrids3x1(Vector3 pos,byte rotateIndex)
    {
        List<GridInEnvanter> grids = new List<GridInEnvanter>();
        if(rotateIndex == 0)
        {
            grids.Add(left.gridInEnvanter);
            grids.Add(this.gridInEnvanter);
            grids.Add(right.gridInEnvanter);
        }
        else if(rotateIndex == 1)
        {
            grids.Add(up.gridInEnvanter);
            grids.Add(this.gridInEnvanter);
            grids.Add(down.gridInEnvanter);
        }
        else if(rotateIndex == 2)
        {
            grids.Add(right.gridInEnvanter);
            grids.Add(this.gridInEnvanter);
            grids.Add(left.gridInEnvanter);
        }
        else
        {
            grids.Add(down.gridInEnvanter);
            grids.Add(this.gridInEnvanter);
            grids.Add(up.gridInEnvanter);
        }
        return grids;
    }    
    public bool CheckGrid3x1(Vector3 pos,int rotateIndex)
    {
        if(rotateIndex == 0)
        {
            if(right == null || left == null)
            {
                return false;
            }
            return true;
        }
        else if(rotateIndex == 1)
        {
            if(up == null || down == null)
            {
                return false;
            }
            return true;
        }
        else if(rotateIndex == 2)
        {
            if(right == null || left == null)
            {
                return false;
            }
            return true;
        }
        else
        {
            if(up == null || down == null)
            {
                return false;
            }
            return true;
        }

        
    }
    public bool CheckGrid4x4(Vector3 pos)
    {
        Direction4 dir = GetDirection4(pos);
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
    public List<Grid> GetGrids4x4(Vector3 pos)
    {
        Direction4 dir = GetDirection4(pos);
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
            }
        }
        else if(dir == Direction4.UpRight)
        {
            if(up != null && right != null && right.up != null)
            {
                grids.Add(up);
                grids.Add(right.up);
                grids.Add(this);
                grids.Add(right);
            }
            
        }
        else if(dir == Direction4.DownLeft)
        {
            if(down != null && left != null && left.down != null)
            {
                grids.Add(left);
                grids.Add(this);
                grids.Add(left.down);
                grids.Add(down);
            }
            
        }
        else if(dir == Direction4.DownRight)
        {
            if(down != null && right != null && right.down != null)
            {
                grids.Add(this);
                grids.Add(right);
                grids.Add(down);
                grids.Add(right.down);
            }
            
        }
        return grids;
    }
    public List<Grid> GetGrids3x1(Vector3 pos,int rotateIndex)
    {
        List<Grid> grids = new List<Grid>();
        if(rotateIndex == 0)
        {
            grids.Add(left);
            grids.Add(this);
            grids.Add(right);
        }
        else if(rotateIndex == 1)
        {
            grids.Add(up);
            grids.Add(this);
            grids.Add(down);

        }
        
        else if(rotateIndex == 2)
        {
            grids.Add(right);
            grids.Add(this);
            grids.Add(left);

        }
        
        else
        {
            grids.Add(down);
            grids.Add(this);
            grids.Add(up);
        }
        return grids;
        // ekleme sırası önemli
        
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
