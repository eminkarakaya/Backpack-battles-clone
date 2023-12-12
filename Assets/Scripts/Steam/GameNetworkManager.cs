using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Steamworks;
using Steamworks.Data;
using Netcode.Transports.Facepunch;
using System;
using System.Threading.Tasks;

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager instance {get;private set;} = null;
    private FacepunchTransport facepunchTransport;
    public Lobby? currentLobby{get;private set;} = null;
    public ulong hostId;
    #region Unity Callbacks

        
    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        } 
    }
    private void Start() {
        facepunchTransport = GetComponent<FacepunchTransport>();
        SteamMatchmaking.OnLobbyCreated += SteamMatchmaking_OnLobbyCreated;
        SteamMatchmaking.OnLobbyEntered += SteamMatchmaking_OnLobbyEntered;
        SteamMatchmaking.OnLobbyEntered += SteamMatchmaking_OnLobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave += SteamMatchmaking_OnLobbyMemberLeave;
        SteamMatchmaking.OnLobbyInvite += SteamMatchmaking_OnLobbyInvite;
        SteamMatchmaking.OnLobbyGameCreated += SteamMatchmaking_OnLobbyGameCreated;
        SteamFriends.OnGameLobbyJoinRequested += SteamFriends_OnGameLobbyJoinRequested;
    }
    private void OnDestroy() {
        
        SteamMatchmaking.OnLobbyCreated -= SteamMatchmaking_OnLobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= SteamMatchmaking_OnLobbyEntered;
        SteamMatchmaking.OnLobbyEntered -= SteamMatchmaking_OnLobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave -= SteamMatchmaking_OnLobbyMemberLeave;
        SteamMatchmaking.OnLobbyInvite -= SteamMatchmaking_OnLobbyInvite;
        SteamMatchmaking.OnLobbyGameCreated -= SteamMatchmaking_OnLobbyGameCreated;
        SteamFriends.OnGameLobbyJoinRequested -= SteamFriends_OnGameLobbyJoinRequested;

        if(NetworkManager.Singleton == null)
            return;
        NetworkManager.Singleton.OnServerStarted -= Singleton_OnServerStarted;
        NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
        NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnected;
    }
    private void OnApplicationQuit() {
        Disconnected();
    }
    #endregion

    #region Steam Callbacks
        
    // when you accept the invite or join on a friend
    private async void SteamFriends_OnGameLobbyJoinRequested(Lobby _lobby, SteamId _steamId)
    {
        RoomEnter joinedLobby = await _lobby.Join();
        Debug.Log(joinedLobby.ToString());
        if(joinedLobby != RoomEnter.Success)
        {
            Debug.Log("failed to create lobby");
        }else{
            currentLobby = _lobby;
            GameManager.instance.ConnectedAsClient();
            Debug.Log("Joined Lobby");
        }
    }

    private void SteamMatchmaking_OnLobbyGameCreated(Lobby lobby, uint arg2, ushort arg3, SteamId id)
    {
        Debug.Log("Lobby Was Created");
        GameManager.instance.SendMessageToChat($"Lobby was created",NetworkManager.Singleton.LocalClientId,true);
    }
    // friends send you a steam invite
    private void SteamMatchmaking_OnLobbyInvite(Friend friend, Lobby lobby)
    {
        Debug.Log($"Invite from{friend.Name}");
    }

    private void SteamMatchmaking_OnLobbyMemberLeave(Lobby lobby, Friend friend)
    {
        Debug.Log("member leave");
        GameManager.instance.SendMessageToChat($"{friend.Name} has left",friend.Id,true);
        NetworkTransmission.instance.RemoveMeFromDictionaryServerRPC(friend.Id);
    } 

    private void SteamMatchmaking_OnLobbyMemberJoined(Lobby lobby)
    {
        Debug.Log("member join");
    }

    private void SteamMatchmaking_OnLobbyCreated(Result result, Lobby lobby)
    {
        if(result != Result.OK)
        {
            Debug.Log("Lobby was not created");
            return;
        }   
        lobby.SetPublic();
        lobby.SetJoinable(true);
        lobby.SetGameServer(lobby.Owner.Id);
        NetworkTransmission.instance.AddMeToDictionaryServerRPC(SteamClient.SteamId,SteamClient.Name,NetworkManager.Singleton.LocalClientId);
    }

    private void SteamMatchmaking_OnLobbyEntered(Lobby lobby)
    {
        if(NetworkManager.Singleton.IsHost)
        {
            return;
        }
        StartClient(currentLobby.Value.Owner.Id);
    }
    #endregion

    #region Netcode Callbacks

        
    public async void StartHost(int maxMembers)
    {
        NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;
        NetworkManager.Singleton.StartHost();
        GameManager.instance.myClientId = NetworkManager.Singleton.LocalClientId;
        currentLobby = await SteamMatchmaking.CreateLobbyAsync(maxMembers);
    }
    public void StartClient(SteamId _sId)
    {
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
        facepunchTransport.targetSteamId = _sId;
        if(NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Client Has Started");
        }
    }

    private void Singleton_OnClientDisconnectCallback(ulong _clientId)
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
        if(_clientId == 0)
        {
            Disconnected();
        }
    }

    private void Singleton_OnClientConnected(ulong _clientId)
    {
        NetworkTransmission.instance.AddMeToDictionaryServerRPC(SteamClient.SteamId,SteamClient.Name,_clientId);
        GameManager.instance.myClientId = _clientId;
        NetworkTransmission.instance.IsTheClientReadyServerRPC(false,_clientId);
        Debug.Log($"Client has connected : {_clientId}");
    }

    private void Singleton_OnServerStarted()
    {
        GameManager.instance.HostCreated();
        Debug.Log("Host Started");
    }
    public void Disconnected()
    {
        currentLobby?.Leave();
        if(NetworkManager.Singleton == null)
        {
            return;
        }
        if(NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.OnServerStarted -= Singleton_OnServerStarted;
        }
        else
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnected;
        }
        NetworkManager.Singleton.Shutdown(true);
        GameManager.instance.ClearChat();
        GameManager.instance.Disconnected();
        Debug.Log("disconnected");
    }
    #endregion

    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture2D = null;
        bool isValid = SteamUtils.GetImageSize(iImage,out uint width , out uint height);
        if(isValid)
        {
            byte[] image = new byte[width*height*4];
            texture2D = new Texture2D((int) width,(int) height,TextureFormat.RGBA32,false,true);
            texture2D.LoadRawTextureData(image);
            texture2D.Apply();
        }
        return texture2D;
    }
    
    public static async Task<Image?> GetAvatar(ulong steamId)
    {
        try
        {
            // Get Avatar using await
            return await SteamFriends.GetLargeAvatarAsync( steamId );
        }
        catch ( Exception e )
        {
            // If something goes wrong, log it
            Debug.Log( e );
            return null;
        }
        
    }
    public static Texture2D Covert(Image image )
    {
        // Create a new Texture2D
        var avatar = new Texture2D( (int)image.Width, (int)image.Height, TextureFormat.ARGB32, false );
        
        // Set filter type, or else its really blury
        avatar.filterMode = FilterMode.Trilinear;

        // Flip image
        for ( int x = 0; x < image.Width; x++ )
        {
            for ( int y = 0; y < image.Height; y++ )
            {
                var p = image.GetPixel( x, y );
                avatar.SetPixel( x, (int)image.Height - y, new UnityEngine.Color( p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f ) );
            }
        }
        
        avatar.Apply();
        return avatar;
    }
    
}
