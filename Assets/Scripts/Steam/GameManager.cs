using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Message
{
    public string text;
    public TMP_Text textObject;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int maxMessages;
    private List<Message> messageList = new List<Message>();
    [SerializeField] private GameObject chatPanel,textObject;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject readyButton,notReadyButton,startButton;
    [SerializeField] private GameObject multiMenu,multiLobby;
    public bool connected,inGame,isHost;
    public Dictionary<ulong,GameObject> playerInfo = new Dictionary<ulong, GameObject>();
    [SerializeField] private GameObject playerFieldBox, playerCardPrefab;
    public ulong myClientId;
    private void Awake() {
        if(instance != null)
        {
            Destroy(this);

        }else{
            instance = this;
        } 
    }
    private void Update() {
        if(inputField.text != "")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(inputField.text == " ")
                {
                    inputField.text = "";
                    inputField.DeactivateInputField();
                    return;
                }
                NetworkTransmission.instance.IWishToSendAChatServerRPC(inputField.text,myClientId);
                // send message
                inputField.text = "";
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                inputField.ActivateInputField();
                inputField.text = " ";
            }
        }
    }
    public void SendMessageToChat(string _text,ulong _fromWho,bool server)
    {
        if(messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message();
        string _name = "Server";

        if(!server)
        {
            if(playerInfo.ContainsKey(_fromWho))
            {
                _name = playerInfo[_fromWho].GetComponent<PlayerInfo>().steamName;
            }
        }

        newMessage.text = name + ": " + _text;
        GameObject newText = Instantiate(textObject,chatPanel.transform);
        newMessage.textObject = newText.GetComponent<TMP_Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }
    public void ClearChat()
    {
        messageList.Clear(); 
        GameObject [] chat = GameObject.FindGameObjectsWithTag("ChatMessage");
        foreach (var item in chat)
        {
            Destroy(item);
        }
        Debug.Log("Clearing Chat");
    }
    public void HostCreated()
    {
        multiMenu.SetActive(false);
        multiLobby.SetActive(true);
        isHost = true;
        connected = true;

    }
    public void ConnectedAsClient()
    {
        multiMenu.SetActive(false);
        multiLobby.SetActive(true);
        isHost = false;
        connected = true;
    }
    public void Disconnected()
    {
        playerInfo.Clear();
        GameObject [] playercards = GameObject.FindGameObjectsWithTag("PlayerCard");
        foreach (var item in playercards)
        {
            Destroy(item);
        }
        multiMenu.SetActive(true);
        multiLobby.SetActive(false);
        isHost = false;
        connected = false;
    }
    public async void AddPlayerToDictionary(ulong _clientId,string _steamName, ulong _steamId)
    {
        if(!playerInfo.ContainsKey(_clientId))
        {
            PlayerInfo _pi = Instantiate(playerCardPrefab,playerFieldBox.transform).GetComponent<PlayerInfo>();
            _pi.steamId = _steamId;

            var avatar = GameNetworkManager.GetAvatar(_steamId);
            await Task.WhenAll( avatar );

            _pi.pp.texture = GameNetworkManager.Covert((Steamworks.Data.Image)avatar.Result);
            _pi.steamName = _steamName;
            playerInfo.Add(_clientId,_pi.gameObject);
        }
    }
    public void UpdateClients()
    {
        foreach (var item in playerInfo)
        {
            ulong _steamId = item.Value.GetComponent<PlayerInfo>().steamId;
            string _steamName = item.Value.GetComponent<PlayerInfo>().steamName;
            ulong _clientId = item.Key;

            NetworkTransmission.instance.UpdateClientsPlayerInfoClientRPC(_steamId,_steamName,_clientId);
        }
    }
    public void RemovePlayerFromDictionaryClientRPC(ulong _steamId)
    {
        GameObject _value = null;
        ulong _key = 100;
        foreach (var item in playerInfo)
        {
            if(item.Value.GetComponent<PlayerInfo>().steamId == _steamId)
            {
                _value = item.Value;
                _key = item.Key;
            }
        }
        if(_key != 100)
        {
            playerInfo.Remove(_key);
        }
        if(_value != null)
        {
            Destroy(_value);
        }
    }
    public void ReadyButton(bool _ready)
    {
        NetworkTransmission.instance.IsTheClientReadyServerRPC(_ready,myClientId);
    }
    public bool CheckIfPlayersAreReady()
    {
        bool _ready = false;
        foreach (var item in playerInfo)
        {
            if(!item.Value.GetComponent<PlayerInfo>().isReady)
            {
                startButton.SetActive(false);
                return false;
            }
            else
            {
                startButton.SetActive(true);
                _ready = true;   
            }
        }
        return _ready;
    }
    public void Quit()
    {
        Application.Quit(); 
    }
    public void StartGameServer()
    {
        if(NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Game",LoadSceneMode.Single);
        }
    }
}
