using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Grid : MonoBehaviour , ISlotable
{
    public Vector2Int index;
    SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnPointerEnter()
    {
        spriteRenderer.color = Color.green;
    }
    public void OnPointerExit()
    {
        spriteRenderer.color = Color.white;
    }
}
