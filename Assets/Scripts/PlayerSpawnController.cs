using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;
using System.Data.Common;
public class PlayerSpawnController : NetworkBehaviour
{

    [SerializeField] private Transform[] playerPositions;


    public GameObject[] playerPrefabs; // Assign your player prefabs in the inspector


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public override void OnNetworkSpawn()
    {
        // NetworkManager.Singleton.SceneManager.OnUnloadEventCompleted -= SpawnPlayer;        SceneManager.sceneLoaded += SpawnPlayers;
        SceneManager.sceneLoaded += SpawnPlayers;

    }

    private void SpawnPlayers(Scene arg0, LoadSceneMode arg1)
    {

        if(IsHost  )
        {

            foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
            {


                GameObject go = Instantiate(playerPrefabs[client.ClientId] , playerPositions[client.ClientId].position ,transform.rotation );
                go.GetComponent<NetworkObject>().SpawnAsPlayerObject(client.ClientId , true);



            }
        }
    }
 
    private void SpawnPlayer(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.Log("we are in " + sceneName);
        if(IsHost && sceneName == "SampleScene" )
        {

            foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
            {


                GameObject go = Instantiate(playerPrefabs[client.ClientId] , playerPositions[client.ClientId].position ,transform.rotation );
                go.GetComponent<NetworkObject>().SpawnAsPlayerObject(client.ClientId , true);



            }
        }
    }


}
