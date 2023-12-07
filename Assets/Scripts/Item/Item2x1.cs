using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2x1 : Item
{
    public override bool CheckGrid(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGridInEnvanter.CheckEnvanterGrid2x1(transform.position,RotateStage);
    }

    public override List<GridInEnvanter> GetGrids(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGridInEnvanter.GetGrids2x1(transform.position,RotateStage);
    }
    public override GridInItem []  GetChildGridsInEnvanterByRotation(int rotationIndex)
    {
        GridInItem []  temp = new GridInItem [gridsInItemDefault.Length];

        if(rotationIndex == 0) // default
        {
            return gridsInItemDefault;
        }
        else if(rotationIndex == 1) // 90 clockwise
        {
            return gridsInItemDefault;
        }
        else if(rotationIndex == 2) // 180
        {
            temp[0] = gridsInItemDefault[1];
            temp[1] = gridsInItemDefault[0];
            return temp;

        }
        else // 270 clockwise
        {
            temp[0] = gridsInItemDefault[1];
            temp[1] = gridsInItemDefault[0];
            return temp;
        }
    }
}
