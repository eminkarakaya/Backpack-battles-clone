using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public enum Prevalence
{
    Common,Rare,Legendary,Unique
}

public class HoverTip : Singleton<HoverTip>
{
    [Header("ItemType and Prevalence")]
    public Prevalence prevalence;
    public ItemType itemType;
    public TMP_Text prevalenceText; 
    public TMP_Text itemTypeText; 

    [Header("WeaponStats")]
    public TMP_Text damageText;
    public TMP_Text cooldownText;

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
    public void SetItemTip(ItemMono item)
    {
        
        itemNameText.text = item.item.itemName;
        SetPrevalenceAndItemType(item.item.prevalence,item.item.itemType);
        if(item.TryGetComponent(out WeaponMono weaponMono))
        {
            weaponStatsObj.SetActive(true);
            damageText.text = weaponMono.weapon.damage.ToString();
            cooldownText.text = weaponMono.weapon.cooldown.ToString();
        }
        else
        {
            weaponStatsObj.SetActive(false);
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