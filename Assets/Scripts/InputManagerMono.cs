using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerMono : Singleton<InputManagerMono>
{
    [SerializeField] private IDragAndDropable dragAndDropable;
    public LayerMask layerMaskItem,layermaskGrid,layermaskEnvanter,layermaskEnvanterGrid;
    InputManager inputManager = new InputManager();
    void Start()
    {
        inputManager.Initialize();
    }
    void Update()
    {
        inputManager.UpdateTick(layerMaskItem,layermaskGrid,layermaskEnvanter,layermaskEnvanterGrid);
    }
}