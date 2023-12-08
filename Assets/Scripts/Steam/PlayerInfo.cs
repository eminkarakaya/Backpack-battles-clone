using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class PlayerInfo : MonoBehaviour
{
    public RawImage pp;
    [SerializeField] private TMP_Text playerName;
    public string steamName;
    public ulong steamId;
    public GameObject readyImage;
    public bool isReady;
    private IEnumerator Start() {
        readyImage.SetActive(false);
        while(steamName == "")
        {
            Debug.Log("steam name null");
            yield return null;
        }
        playerName.text = steamName;
    }
}
