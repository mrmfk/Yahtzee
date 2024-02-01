using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class DiceAnimation : NetworkBehaviour
{
    public Sprite[] diceSprites;
    public Image diceImage;
    public GameObject image;
    public GameObject image2;
    public float animationDuration = 0.8f; 
    public int faceValue;
    public bool isLocked;
    void Start()
    {

    }

    IEnumerator RollDiceAnimation()
    {
 
        int counter = 0;
       while(counter< 12)
        {
            int randomIndex = Random.Range(0,6); 
            diceImage.sprite = diceSprites[randomIndex]; 
            counter++;


            yield return new WaitForSeconds(0.1f); ; 
        }
         faceValue = Random.Range(0, 6);
        diceImage.sprite = diceSprites[faceValue]; 

    
    }
    public void Roll()
    {
        if (!isLocked)
        {
            StartCoroutine(RollDiceAnimation());

        }
    }

    public void ToggleLock()
    {
        if (isLocked) 
        image2.SetActive(false);
        else image2.SetActive(true);
        isLocked = !isLocked;

    
    }
   
  
}

