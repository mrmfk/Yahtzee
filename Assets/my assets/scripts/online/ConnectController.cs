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
    }

    void Update()
    {

    }

 


    public void closeScene(){
        SceneManager.LoadScene("menu");
    }
    
    public void startgameClick()
    {
        connectPage.SetActive(false);
        CloseForClientsClientRpc();
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
