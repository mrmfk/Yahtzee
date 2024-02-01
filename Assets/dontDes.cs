using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;

public class dontDes : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        SceneManager.sceneLoaded += setActive; 
    }

    private void setActive(Scene arg0, LoadSceneMode arg1)
    {
        this.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
