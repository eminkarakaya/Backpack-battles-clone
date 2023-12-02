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
    /// <summary>
    ///
    /// 90 da 
    ///     0 - 2
    ///     1 - 0
    ///     2 - 3
    ///     3 - 1
    /// 180 de
    ///     0 - 3
    ///     1 - 2
    ///     2 - 1
    ///     3 - 0
    /// 270 de
    ///     0 - 1
    ///     1 - 3 
    ///     2 - 0
    ///     3 - 2
    ///  
    /// </summary>

    public override GridInEnvanter[] GetChildGridsInEnvanterByRotation(int rotationIndex)
    {
        GridInEnvanter[] temp = new GridInEnvanter[childGridsInEnvanterDefault.Length];

        if(rotationIndex == 0) // default
        {
            return childGridsInEnvanterDefault;
        }
        else if(rotationIndex == 1) // 90 clockwise
        {
            temp[0] = childGridsInEnvanterDefault[2];
            temp[1] = childGridsInEnvanterDefault[0];
            temp[2] = childGridsInEnvanterDefault[3];
            temp[3] = childGridsInEnvanterDefault[1];
            return temp;
        }
        else if(rotationIndex == 2) // 180
        {
            temp[0] = childGridsInEnvanterDefault[3];
            temp[1] = childGridsInEnvanterDefault[2];
            temp[2] = childGridsInEnvanterDefault[1];
            temp[3] = childGridsInEnvanterDefault[0];
            return temp;

        }
        else // 270 clockwise
        {
            temp[0] = childGridsInEnvanterDefault[1];
            temp[1] = childGridsInEnvanterDefault[3];
            temp[2] = childGridsInEnvanterDefault[0];
            temp[3] = childGridsInEnvanterDefault[2];
            return temp;
        }
    } 
}
