using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInEnvanter : MonoBehaviour, ISlotForEnvanter
{
    public Envanter envanter;
    public Grid grid;
    SpriteRenderer spriteRenderer;
    private void Start() {
        envanter = GetComponentInParent<Envanter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OpenPutableColor()
    {
        spriteRenderer.color = Color.blue;
    }
    public void ClosePutableColor()
    {
        spriteRenderer.color = Color.gray;
    }
    
    public void OnPointerEnter()
    {
        spriteRenderer.color = Color.green;
        EnvanterSystem.Instance.selectedGridInEnvanter = this;
    }

    public void OnPointerExit()
    {
        spriteRenderer.color = Color.gray;
        EnvanterSystem.Instance.selectedGridInEnvanter = null;
    }
}
