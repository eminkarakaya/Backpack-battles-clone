using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter3x1 : Envanter
{
    public override bool CheckGrid(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGrid.CheckGrid3x1(transform.position,RotateStage);
    }

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
            temp[1] = childGridsInEnvanterDefault[1];
            temp[2] = childGridsInEnvanterDefault[0];
            return temp;
        }
        else if(rotationIndex == 2) // 180
        {
            return childGridsInEnvanterDefault;
        }
        else // 270 clockwise
        {
            temp[0] = childGridsInEnvanterDefault[2];
            temp[1] = childGridsInEnvanterDefault[1];
            temp[2] = childGridsInEnvanterDefault[0];
            return temp;
        }
    }

    public override List<Grid> GetGrids(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGrid.GetGrids3x1(transform.position,RotateStage);
    }
}
