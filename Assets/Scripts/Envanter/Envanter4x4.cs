using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter4x4 : Envanter
{
    public override List<Grid> GetGrids(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGrid.GetGrids4x4(transform.position);
    }
     public override bool CheckGrid(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGrid.CheckGrid4x4(transform.position);
    }
}
