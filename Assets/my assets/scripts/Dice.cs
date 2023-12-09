using UnityEngine;

public class Dice : MonoBehaviour
{
    public int faceValue;
    public bool isLocked;

    public void Roll()
    {
        if (!isLocked)
        {
            faceValue = Random.Range(1, 7);
            Debug.Log("Die rolled: " + faceValue);
        }
    }

    public void ToggleLock()
    {
        isLocked = !isLocked;
        Debug.Log("Die is " + (isLocked ? "locked" : "unlocked"));
    }




}
