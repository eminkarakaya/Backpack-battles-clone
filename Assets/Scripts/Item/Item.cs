using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject outline;

    
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
    private void OnMouseDown() {
        // Debug.Log(this + " item",this);
    }
    private void OnMouseDrag() {
        
    }

    public void OnDrag()
    {
        // Debug.Log(nameof(OnDrag));
        transform.position = MouseWorldPosition();
    }

    public void OnTouchEnd()
    {
        // Debug.Log(nameof(OnTouchEnd));
        outline.SetActive(false);
    }

    public void Select()
    {
        // Debug.Log(nameof(Select));
        outline.SetActive(true);
    }
}
