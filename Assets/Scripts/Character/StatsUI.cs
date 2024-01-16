using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text goldText,classText,healthText,roundText,winText,triesText;
    [SerializeField] private Character character;
    public void StartUI()
    {
        nameText.text = character.nickName;
        classText.text = character.characterClass;
        goldText.text = character.currentGold.ToString();
        healthText.text = character.currentHealth.ToString();

    }
    public void UpdateUI()
    {
        goldText.text = character.currentGold.ToString();
        healthText.text = character.currentHealth.ToString();
    }
}
