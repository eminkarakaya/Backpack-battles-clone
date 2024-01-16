using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public int startingHealth;
    public float healthIncreasePercentagePerRound;
    public string name;
    public int GetHealthByRound(int round)
    {
        return (int) (startingHealth * healthIncreasePercentagePerRound * round);
    }

}
public class Character : MonoBehaviour
{
    public int currentHealth;
    public int currentGold;
    public string nickName;
    public string characterClass;
    CharacterData characterData;
    private void Start() {
        characterData = new CharacterData();
    }

    private void UpdateCharacterData(int round)
    {
        currentHealth = characterData.GetHealthByRound(round);
    }
    
}
