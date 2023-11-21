using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter : MonoBehaviour, IDragAndDropable,IEnvantable
{
    // [SerializeField] private int freeLayer,placedLayer;
    BoxCollider2D [] childColliders;
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject outline;
    private void Start() {
        // gameObject.layer = freeLayer;
        childColliders = GetComponentsInChildren<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
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
        PutInSlotMap();
        if(outline != null) 
            outline.SetActive(false);
    }

    public void Select()
    {
        if(outline != null) 
            outline.SetActive(true);
    }
    private void OpenColliders()
    {
        foreach (var item in childColliders)
        {
            item.enabled = true;
        }
    }
    private void CloseColliders()
    {
        foreach (var item in childColliders)
        {
            item.enabled = false;
        }
    }
    private void ChangeFreeLayer()
    {
        // gameObject.layer = placedLayer;
    }
    private void ChangePlacedLayer()
    {
        // gameObject.layer = freeLayer;
    }
    public void PutInSlotMap()
    {
        if(EnvanterSystem.Instance.selectedGrid!= null)
        {
            transform.position = EnvanterSystem.Instance.selectedGrid.transform.position;
            EnvanterSystem.Instance.selectedGrid.TriggerOnPointerExit();
            OpenColliders();
            ChangePlacedLayer();
        }
    }

    public void PutInSlotMapError()
    {
        
    }

    public void TakeItOutSlotMap()
    {
        
    }
}
