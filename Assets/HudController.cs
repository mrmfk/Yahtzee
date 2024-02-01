using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class HudController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRetry()
    {
        if(IsServer)
        {

            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }

    }

    public void OnClickMainMenu()
    {
         Application.Quit();   


    }
}
