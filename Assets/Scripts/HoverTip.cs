using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public enum Prevalence
{
    Common,Rare,Legendary,Unique
}

public class WeaponStatsUI
{
    public TMP_Text damageText,cooldown;
}
public class HoverTip : Singleton<HoverTip>
{
    [Header("ItemType and Prevalence")]
    public Prevalence prevalence;
    public ItemType itemType;
    public TMP_Text prevalenceText; 
    public TMP_Text itemTypeText; 

    [Header("UI Objects")]
    [SerializeField] private GameObject tipObj;
    [SerializeField] private GameObject weaponStatsObj;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    public void OpenTipObj()
    {
        tipObj.SetActive(true);
    }
    public void CloseTipObj()
    {
        tipObj.SetActive(false);
    }
    public void SetItemTip(Item item)
    {
        itemNameText.text = item.itemName;
        SetPrevalenceAndItemType(item.prevalence,item.itemType);
        if(item is Weapon)
        {
            
        }
    }
    public void SetPrevalenceAndItemType(Prevalence prevalence,ItemType itemType)
    {
        this.itemType = itemType;
        this.prevalence = prevalence;
        prevalenceText.text = prevalence.ToString();
        itemTypeText.text = itemType.ToString();
    }
}