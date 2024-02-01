using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;


public class ConnectController : NetworkBehaviour
{


    void Start()
    {

    }

    void Update()
    {

    }
    public void closeScene(){
        SceneManager.LoadScene("menu");
    }
}
