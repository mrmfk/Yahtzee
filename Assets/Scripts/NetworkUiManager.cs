using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using System.Net;
using System.Linq;
using UnityEngine.UI;
using System.Security.Principal;
using Unity.Mathematics;
public class NetworkUiManager : NetworkBehaviour
{
    [SerializeField]TMP_InputField IP;
    [SerializeField] private TextMeshProUGUI ipText;

    [SerializeField] GameObject playCanvas ; 
    [SerializeField] GameObject guideCanvas ; 
    [SerializeField] GameObject aboutCanvas ; 

    [SerializeField] Button shutdownButton;
    [SerializeField] Button hostButton;
    [SerializeField] Button startGameButton;
    [SerializeField] Button clientButton;

    [SerializeField] GameObject popUpTextPrefab; 
    [SerializeField] TMP_Text popUpText;
    [SerializeField] Transform popUpTextPos;
    

    

    String ipAdress = "127.0.0.1";

    
    void Start()
    {
        // IP = FindObjectOfType<TMP_InputField>();
        Debug.Log(GetLocalIPv4());


        ipText.text = "IP : " + GetLocalIPv4();
         
    }

    

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect; 
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
            foreach(NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
            {
                HandleClientConnect(client.ClientId);
            }
        }

        if(NetworkManager.Singleton.LocalClientId  == 1 )
        {
            NetworkManager.Singleton.OnClientConnectedCallback += conected;
            NetworkManager.Singleton.OnClientDisconnectCallback += Disconected;
        }
    }

    private void Disconected(ulong obj)
    {
        popUpTextInit(Color.red , "Invalid IP Adress!!");
    }

    private void conected(ulong obj)
    {
        Debug.Log("You Connected");
        popUpTextInit(Color.green , "You Connected To The Host!!");
    }

    public override void OnNetworkDespawn()
    {
        if(IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect; 
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;

        }
    }


    private void HandleClientConnect(ulong clientID)
    {
        Debug.Log("client" + clientID + " connected!");
        if(clientID == 0)
        {
            popUpTextInit(Color.green , "Share Your IP to Your Friend");
        }
        else{

            popUpTextInit(Color.green , "A Player Connected To Host!!");
        }
    }

    private void HandleClientDisconnect(ulong clientID)
    {
        Debug.Log("client" + clientID + " Disconnected!");
        popUpTextInit(Color.red , "A Player Disonnected!!");
    }
    void Update()
    {
        
    }
    public void OnClickHost()
    {
        // hostButton.gameObject.SetActive(false);
        // shutdownButton.gameObject.SetActive(true);
        clientButton.enabled = false;
        ipAdress = GetLocalIPv4();
        NetworkManager.GetComponent<UnityTransport>().ConnectionData.Address=ipAdress;
        NetworkManager.Singleton.StartHost();
        startGameButton.gameObject.SetActive(true);
        
    }

    public void OnClickClient()
    {
        hostButton.enabled = false;

        try
        {
            if(IP.text!="")
            {
                ipAdress = IP.text;

            }
            NetworkManager.GetComponent<UnityTransport>().ConnectionData.Address=ipAdress;
            // NetworkManager.GetComponent<UnityTransport>().ConnectionData.Port = 
            Debug.Log(IP.text);

            NetworkManager.Singleton.StartClient();

            // SceneManager.LoadScene(0);
            
        }
        catch (Exception ex)
        {
            popUpTextInit(Color.red , "Invalid IP Adress!!");
        }
        
    }

    void ChangeScene() 
    {
        NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

    }

    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.First(
        f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .ToString();
    }

    public void OnClickRefresh ()
    {
        ipText.text = "IP : "+ GetLocalIPv4();
        popUpTextInit(Color.green , "IP Refreshed!!");
    }

    public void OnClickPlay()
    {
        playCanvas.SetActive(true);
        guideCanvas.SetActive(false);
    }
    public void OnClickGuide()
    {
        playCanvas.SetActive(false);
        guideCanvas.SetActive(true);
    }
    public void OnClickQuit()
    {
        Application.Quit();   
    }
    public void OnClicShutdown()
    {
        // hostButton.gameObject.SetActive(true);
        // shutdownButton.gameObject.SetActive(false);
        // startGameButton.gameObject.SetActive(false);
        // if(IsServer)
        // {

        //     NetworkManager.Singleton.Shutdown();
        // }

    }
    public void OnClickStart()
    {
        if(!IsServer){return;}
        Debug.Log("connected players : "+NetworkManager.ConnectedClients.Count);
        if(NetworkManager.ConnectedClients.Count > 1)
        {
            ChangeScene();
                
        }
        else
        {
            popUpTextInit(Color.red , "Not Enough Players!!");
        }
    }

    void popUpTextInit(Color color , string text)
    {
        popUpText.text = text;
        popUpText.color = color;
        Instantiate(popUpTextPrefab , popUpTextPos.position , quaternion.identity , this.gameObject.transform.GetChild(0).transform );
    }

    public void OnbuttonDown()
    {
        aboutCanvas.SetActive(true);
    }
    public void OnbuttonUp()
    {
        aboutCanvas.SetActive(false);   
    }
    // public void OnClickQuit()
    // {
    //     Application.Quit();   
    // }

}
