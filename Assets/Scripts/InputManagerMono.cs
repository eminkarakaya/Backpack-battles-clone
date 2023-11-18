using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerMono : MonoBehaviour
{
    public LayerMask layerMaskItem,layermaskGrid;
    InputManager inputManager = new InputManager();
    void Start()
    {
        inputManager.Initialize();
    }
    void Update()
    {
        inputManager.UpdateTick(layerMaskItem,layermaskGrid);
    }
}