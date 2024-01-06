using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public int health;
    public string name;
}
public class Character : MonoBehaviour
{
    CharacterData characterData;
    private void Start() {
        characterData = new CharacterData();
    }
}
