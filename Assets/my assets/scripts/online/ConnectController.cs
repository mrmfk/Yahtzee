using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using TMPro;
using Unity.Mathematics;

public class ConnectController : NetworkBehaviour
{
    [SerializeField] GlobalGameManager globalGameManager;
    [SerializeField] GameObject connectPage;
    [SerializeField] GameObject startGameObj;
    [SerializeField] GameObject Clientbtn;
    [SerializeField] GameObject Hostbtn;
    [SerializeField] GameObject textObj;
    [SerializeField] GameObject refreshObj;
    [SerializeField] GameObject ipObj;
    [SerializeField] GameObject canvasGameObject;

    [SerializeField] GameObject popUpTextPrefab;
    [SerializeField] TMP_Text popUpText;
    [SerializeField] Transform popUpTextPos;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] TMP_InputField IP;
   // public NetworkList <int> test ;
        // public NetworkVariable<bool>[] myTurn = new NetworkVariable<int> [10](true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    String ipAdress = "127.0.0.1";
    void Start()
    {
        // test = new NetworkList<int>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        ipText.text = "IP : " + GetLocalIPv4();
    }

    void Update()
    {

    }


    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect;
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
            foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
            {
                HandleClientConnect(client.ClientId);
            }
        }

        if (NetworkManager.Singleton.LocalClientId == 1)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += conected;
            NetworkManager.Singleton.OnClientDisconnectCallback += Disconected;
        }
    }

    private void Disconected(ulong obj)
    {
        popUpTextInit(Color.red, "Invalid IP Adress!!");
    }

    private void conected(ulong obj)
    {
        Debug.Log("You Connected");
        popUpTextInit(Color.green, "You Connected To The Host!!");
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;

        }
    }


    private void HandleClientConnect(ulong clientID)
    {
        Debug.Log("client" + clientID + " connected!");
        if (clientID == 0)
        {
            popUpTextInit(Color.green, "Share Your IP to Your Friend");
        }
        else
        {

            popUpTextInit(Color.green, "A Player Connected To Host!!");
        }
    }

    private void HandleClientDisconnect(ulong clientID)
    {
        Debug.Log("client" + clientID + " Disconnected!");
        popUpTextInit(Color.red, "A Player Disonnected!!");
    }

    public void closeScene(){
       /* if (IsClient)
         {
            NetworkManager.Singleton.Shutdown();
            popUpTextInit(Color.red, "A player exit the game!!");
            if(IsHost)
             NetworkManager.SceneManager.LoadScene("menu", LoadSceneMode.Single);
            else

                SceneManager.LoadScene("menu");
        }*/

        //NetworkManager.SceneManager.LoadScene("multiPlayerScene", LoadSceneMode.Single);

        SceneManager.LoadScene("menu");
    }
    
    public void startgameClick()
    {

        if (!IsServer) { return; }
        Debug.Log("connected players : " + NetworkManager.ConnectedClients.Count);
        if (NetworkManager.ConnectedClients.Count > 1)
        {
            connectPage.SetActive(false);
            CloseForClientsClientRpc();

        }
        else
        {
            popUpTextInit(Color.red, "Not Enough Players!!");
        }
    }
    [ClientRpc]
    void CloseForClientsClientRpc()
    {
        connectPage.SetActive(false);
    }
    public void OnClickHost()
    {
        Clientbtn.SetActive(false);
        textObj.SetActive(false);
        Hostbtn.SetActive(false);
        startGameObj.SetActive(true);
        NetworkManager.Singleton.StartHost();
        
    }
    public void OnClickclient()
    {
        try
        {
            if (IP.text != "")
            {
                ipAdress = IP.text;

            }
            NetworkManager.GetComponent<UnityTransport>().ConnectionData.Address = ipAdress;
            NetworkManager.Singleton.StartClient();
           // startGameObj.SetActive(true);
            ipObj.SetActive(false);
            refreshObj.SetActive(false);
            Hostbtn.SetActive(false);
            Clientbtn.SetActive(false);

        }
        catch (Exception ex)
        {
            popUpTextInit(Color.red, "Invalid IP Adress!!");
        }

    }
    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.First(
        f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .ToString();
    }

    public void OnClickRefresh()
    {
        ipText.text = "IP : " + GetLocalIPv4();
        popUpTextInit(Color.green, "IP Refreshed!!");
    }
   public  void popUpTextInit(Color color, string text)
    {
        popUpText.text = text;
        popUpText.color = color;
        Instantiate(popUpTextPrefab, popUpTextPos.position, quaternion.identity, canvasGameObject.transform);
    }


}
