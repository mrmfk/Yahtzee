using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{

    public GameObject helpObj;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    public void playLocal()
    {
        //NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        SceneManager.LoadScene("SampleScene");


    }
    public void playOnline()
    {
        //NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        SceneManager.LoadScene("multiPlayerScene");


    }
    public void closeHelp()
    {
        helpObj.SetActive(false);
    }
    public void openHelp()
    {
        helpObj.SetActive(true);
    }
    public void exxitApp()
    {
        Application.Quit();
    }
}
