using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;


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
    


    void Start()
    {

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
    }
    public void OnClickHost()
    {
        Clientbtn.SetActive(false);
        textObj.SetActive(false);
        Hostbtn.SetActive(false);
        NetworkManager.Singleton.StartHost();
        startGameObj.SetActive(true);
    }
    public void OnClickclient()
    {
        NetworkManager.Singleton.StartClient();
        startGameObj.SetActive(true);
        ipObj.SetActive(false);
        refreshObj.SetActive(false);
        Hostbtn.SetActive(false);
        Clientbtn.SetActive(false);
    }

    public void OnClickTestTurn()
    {
        if(IsHost && globalGameManager.myTurn.Value)
        {
            Debug.Log("Its Host Turn");
            globalGameManager.SwitchTurnServerRpc();
        }
        else if(!IsHost && !globalGameManager.myTurn.Value)
        {
            Debug.Log("Its Clients Turn");
            globalGameManager.SwitchTurnServerRpc();
        }
    }
}
