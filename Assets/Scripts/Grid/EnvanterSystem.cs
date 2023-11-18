using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvanterSystem : MonoBehaviour
{
    [SerializeField] Vector2 offsetMultiplier;
    [SerializeField] private Grid _gridPrefab;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private List<Grid> allGrids;
    [SerializeField] private Vector3 scale;
    [SerializeField] private Vector2 _startPos;

    private void Start() {
        CreateGrids();
    }
    private Grid CreateGrid(Vector2Int index,Vector2 pos)
    {
        var obj = Instantiate(_gridPrefab,pos,Quaternion.identity,_gridParent);
        obj.index = index;
        obj.name = "Grid "+index;
        allGrids.Add(obj);
        return obj;
    }
   
    [ContextMenu("CreateGrids")]
    private void CreateGrids()
    {
        for (int y = 0; y < scale.y; y++)
        {
            for (int x = 0; x < scale.x; x++)
            {
                Grid grid = CreateGrid(new Vector2Int(x,y),new Vector2(_startPos.x + x *offsetMultiplier.x,_startPos.y - y*offsetMultiplier.y));
            }
        }
    }

    // public Grid FindGridByPos(Vector3 position)
    // {

    // }
}
