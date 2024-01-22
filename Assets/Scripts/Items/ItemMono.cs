using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMono : MonoBehaviour
{
    public Item item;
    public virtual void OnTriggerEnter()
    {
        item.OnTriggerEnter();
        HoverTip.Instance.SetItemTip(this);
    }
    public virtual void OnTriggerExit()
    {
        item.OnTriggerExit();
        
    }
}
