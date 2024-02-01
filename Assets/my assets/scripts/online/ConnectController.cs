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


    void Start()
    {

    }

    void Update()
    {

    }
    public void closeScene(){
        connectPage.SetActive(false);
    }
    public void OnClickHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void OnClickclient()
    {
        NetworkManager.Singleton.StartClient();
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
