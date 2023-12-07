using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3x1 : Item
{
    public override bool CheckGrid(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGridInEnvanter.CheckEnvanterGrid3x1(transform.position,RotateStage);
    }

    public override GridInItem[] GetChildGridsInEnvanterByRotation(int rotationIndex)
    {
        GridInItem []  temp = new GridInItem [gridsInItemDefault.Length];

        if(rotationIndex == 0) // default
        {
            return gridsInItemDefault;
        }
        else if(rotationIndex == 1) // 90 clockwise
        {
            temp[0] = gridsInItemDefault[2];
            temp[1] = gridsInItemDefault[1];
            temp[2] = gridsInItemDefault[0];
            return temp;
        }
        else if(rotationIndex == 2) // 180
        {
            return gridsInItemDefault;
        }
        else // 270 clockwise
        {
            temp[0] = gridsInItemDefault[2];
            temp[1] = gridsInItemDefault[1];
            temp[2] = gridsInItemDefault[0];
            return temp;
        }
    }

    public override List<GridInEnvanter> GetGrids(Vector3 pos)
    {
        return EnvanterSystem.Instance.selectedGridInEnvanter.GetGrids3x1(transform.position,RotateStage);
    }
}
