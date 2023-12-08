using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;

public class NetworkTransmission : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public static NetworkTransmission instance;
    private void Awake() {
        if(instance != null)
        {
            Destroy(this);

        }else{
            instance = this;
        } 
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneLoaded;
    }

    private void SceneLoaded(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if(IsHost && sceneName == "Game")
        {
            foreach (var item in clientsCompleted)
            {
                GameObject player = Instantiate(playerPrefab);
                player.GetComponent<NetworkObject>().SpawnWithOwnership(item,true);
            }
        }
    }

    [ServerRpc(RequireOwnership =false)]
    public void IWishToSendAChatServerRPC(string _message,ulong _fromWho)
    {
        ChatFromServerClientRPC(_message,_fromWho);
    }
    [ClientRpc]
    private void ChatFromServerClientRPC(string _message, ulong _fromWho)
    {
        GameManager.instance.SendMessageToChat(_message,_fromWho,false);
    }
    [ServerRpc(RequireOwnership = false)]
    public void AddMeToDictionaryServerRPC(ulong _steamId, string _steamName, ulong _clientId)
    {
        GameManager.instance.SendMessageToChat($"{_steamName} has joined" , _clientId,true);
        GameManager.instance.AddPlayerToDictionary(_clientId,_steamName,_steamId);
        GameManager.instance.UpdateClients();
    }
    [ServerRpc(RequireOwnership = false)]
    public void RemoveMeFromDictionaryServerRPC(ulong _steamId)
    {
        RemovePlayerFromDictionaryClientRPC(_steamId);
    }
    [ClientRpc]
    private void RemovePlayerFromDictionaryClientRPC(ulong _steamId)
    {
        Debug.Log("removing client");
        GameManager.instance.RemovePlayerFromDictionaryClientRPC(_steamId);
    }
    [ClientRpc] 
    public void UpdateClientsPlayerInfoClientRPC(ulong _steamId,string _steamName, ulong _clientId)
    {
        GameManager.instance.AddPlayerToDictionary(_clientId,_steamName,_steamId);
    }
    [ServerRpc(RequireOwnership = false)]
    public void IsTheClientReadyServerRPC(bool _ready, ulong _clientId)
    {
        AClientMightBeReadyClientRPC(_ready,_clientId);
    }
    [ClientRpc]
    private void AClientMightBeReadyClientRPC(bool _ready , ulong _clientId)
    {
        foreach (var item in GameManager.instance.playerInfo)
        {
            if(item.Key == _clientId)
            {
                item.Value.GetComponent<PlayerInfo>().isReady = _ready;
                item.Value.GetComponent<PlayerInfo>().readyImage.SetActive(_ready);
                if(NetworkManager.Singleton.IsHost)
                {
                    Debug.Log(GameManager.instance.CheckIfPlayersAreReady());
                    // check ıf players are ready
                }
            }
        }
    }
}
