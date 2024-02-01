using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : NetworkBehaviour
{
    public NetworkVariable<int> myTurn  = new NetworkVariable<int>(0 , NetworkVariableReadPermission.Everyone , NetworkVariableWritePermission.Owner );
    [SerializeField] public NetworkVariable<bool> endMatchNet ;
    [SerializeField] bool endMatch = false;
    int temp;
    [SerializeField] float delayTurn = 1;

    [SerializeField] GameObject lostHud;
    [SerializeField] GameObject winHud;
    [SerializeField] GameObject pausetHud;

    [SerializeField] GameObject popUpTextPrefab; 
    [SerializeField] TMP_Text popUpText;
    [SerializeField] Transform popUpTextPos;

    [SerializeField]public TMP_Text turnText;
    [SerializeField]public TMP_Text p1Scoretext;
    [SerializeField]public TMP_Text p2Scoretext;
    [SerializeField]public TMP_Text p1PauseScoretext;
    [SerializeField]public TMP_Text p2PauseScoretext;
    [SerializeField] public int p1Score = 0;
    [SerializeField] public int p2Score = 0;
    public bool pauseClicked = false;
   [SerializeField] GameObject pauseBTN;




public override void OnNetworkSpawn()
    {
        
        NetworkManager.SceneManager.OnLoadComplete +=ActiveTurnText;
        

 
    }

    private void ActiveTurnText(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        if(sceneName == "SampleScene")
        {
            turnText.gameObject.SetActive(true);
            // pauseBTN.SetActive(true);
        }
        else
        {
            turnText.gameObject.SetActive(false);
            // pauseBTN.SetActive(false);
        }
    }

    void Start()
  {
      DontDestroyOnLoad(this.gameObject);
  }
    void Update() 
    {
        ChengeTurnText();
        if(endMatchNet.Value)
        {
            EnableLostHud();
        }
        else
        {
            lostHud.SetActive(false);
        }
    }

    private void ChengeTurnText()
    {
        if(myTurn.Value == 0)
        {
            turnText.text = "P1";
            turnText.color =  new Color(0 , 0.4941176f , 0.2862745f , 1); 
        }
        else if(myTurn.Value == 1)
        {
            turnText.text = "P2";
            turnText.color =  new Color(0.1058824f , 0.1843137f , 0.7176471f , 1); 
        }
        else 
        {
            turnText.text = "Wait";
            turnText.color =  new Color(0.2509804f , 0.1803922f , 0.1960784f , 1); 
        }
    }

    [ServerRpc(RequireOwnership =false)]
    public void SwitchTurnServerRpc ()
    {
        StartCoroutine("DelayTurn");
    }

    IEnumerator DelayTurn()
    {
         if(myTurn.Value==0  )
        {
            myTurn.Value = 2;
            yield return new WaitForSeconds(delayTurn);
            
            myTurn.Value = 1;
        }
        else if(myTurn.Value ==1  )
        {
            myTurn.Value = 2;
            yield return new WaitForSeconds(delayTurn);
            
            myTurn.Value = 0;
        } 
        
    }

     public void OnClickRetry()
    {
        if(IsServer)
        {

            EndMatchClientRpc(endMatch);
            lostHud.SetActive(false);
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            endMatch = false;
        }
        else
        {
            popUpTextInit(Color.red , "Only Host Can Restart The Game!");
        }

    }

    public void OnClickMainMenu()
    {
        Application.Quit();   


    }

    public void EnableLostHud()
    {
        lostHud.SetActive(true);
    }
    
    public void OnClickPause()
    {
        p1PauseScoretext.text = p1Score.ToString();
        p2PauseScoretext.text = p2Score.ToString();
        if(pauseClicked  == false)
        {
            pausetHud.SetActive(true);
            pauseClicked = true;
            
        }
        else if(pauseClicked == true)
        {
            pausetHud.SetActive(false);
            pauseClicked = false;
        }
    }

       [ClientRpc]
    public void EndMatchClientRpc(bool end)
    {
        p1Scoretext.text = p1Score.ToString();
        p2Scoretext.text = p2Score.ToString();
        endMatchNet = new NetworkVariable<bool>(end);
    }

       [ClientRpc]
    public void ChangeScoreClientRpc(ulong client )
    {
        if(client == 0)
        {
            p2Score ++;
        }
        if(client == 1)
        {

            p1Score ++; 
        }
    }

    void popUpTextInit(Color color , string text)
    {
        popUpText.text = text;
        popUpText.color = color;
        Instantiate(popUpTextPrefab , popUpTextPos.position , quaternion.identity , this.gameObject.transform.GetChild(0).transform );
    }

    
}
