using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public List<int> scores; 
    public int totalScore; 
    public bool[] diceLocked; 

    public Player(string name)
    {
        playerName = name;
        scores = new List<int>();
        diceLocked = new bool[5];
    }

    public void RollDice(Dice[] dice)
    {
        for (int i = 0; i < dice.Length; i++)
        {
            if (!diceLocked[i])
            {
                dice[i].Roll();
            }
        }
    }

    public void ToggleDiceLock(int index)
    {
        diceLocked[index] = !diceLocked[index];
        Debug.Log(playerName + "'s die " + (index + 1) + " is " + (diceLocked[index] ? "locked" : "unlocked"));
    }

    public void CalculateTotalScore()
    {
        totalScore = 0;
        foreach (int score in scores)
        {
            totalScore += score;
        }
    }
}

