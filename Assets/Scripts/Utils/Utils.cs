using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static RaycastHit2D CastRay( Vector3 pos ,int layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity,layerMask);
        return hit;
    }
}