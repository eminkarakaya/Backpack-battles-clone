using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter3x1 : Envanter
{
    public override bool CheckGrid(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGrid.CheckGrid3x1(transform.position);
    }

    public override List<Grid> GetGrids(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGrid.GetGrids3x1(transform.position);
    }
}
