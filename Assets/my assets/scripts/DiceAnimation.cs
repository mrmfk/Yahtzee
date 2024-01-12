using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceAnimation : MonoBehaviour
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
       // StartCoroutine(RollDiceAnimation()); // شروع اجرای انیمیشن
    }

    IEnumerator RollDiceAnimation()
    {
        //float elapsedTime = 0.5f; // زمان گذشته شده در انیمیشن
        int counter = 0;
        //while (elapsedTime < animationDuration)
       while(counter< 12)
        {
            int randomIndex = Random.Range(0,6); // انتخاب تصادفی یک اسپرایت تاس
            diceImage.sprite = diceSprites[randomIndex]; // تغییر اسپرایت تاس
            counter++;
           // elapsedTime += Time.deltaTime; // افزایش زمان گذشته

            yield return new WaitForSeconds(0.1f); ; // صبر برای یک فریم
        }
         faceValue = Random.Range(0, 6);
        diceImage.sprite = diceSprites[faceValue]; // قرار دادن تاس بر روی عدد خاص
      
      //  diceImage.enabled=false;
       // diceImage.enabled=true;
    
    }
    public void Roll()
    {
        if (!isLocked)
        {
            StartCoroutine(RollDiceAnimation());
            // faceValue = Random.Range(1, 7);
            // Debug.Log("Die rolled: " + faceValue);
        }
    }

    public void ToggleLock()
    {
        if (isLocked) 
        image2.SetActive(false);
        else image2.SetActive(true);
        isLocked = !isLocked;
        Debug.Log("Die is " + (isLocked ? "locked" : "unlocked"));
    
    }
   
  
}

