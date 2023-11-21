using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragAndDropable , ISlotForItem
{
    [SerializeField] private GameObject outline;
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    public void OnDrag()
    {
        transform.position = MouseWorldPosition();
    }

    public void OnTouchEnd()
    {
        PutInSlot();
        outline.SetActive(false);
    }

    public void Select()
    {
        outline.SetActive(true);
    }

    public void PutInSlot()
    {
        if(EnvanterSystem.Instance.selectedGridInEnvanter != null)
        {
            transform.position = EnvanterSystem.Instance.selectedGridInEnvanter.transform.position;
        }
    }

    public void OnPointerExit()
    {
        
    }

    public void OnPointerEnter()
    {
        
    }
}
