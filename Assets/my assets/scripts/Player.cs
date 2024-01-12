using System;
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


    public void ScoreInCategory(int category, int[] diceValues)
    {
        int score = 0;

        switch (category)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                score = CountDiceWithValue(diceValues, category);
                break;
            case 7: // Three of a Kind
                score = GetNOfAKindScore(diceValues, 3);
                break;
            case 8: // Four of a Kind
                score = GetNOfAKindScore(diceValues, 4);
                break;
            case 10: // Full House
                score = GetFullHouseScore(diceValues);
                break;
            case 11: // Small Straight
                score = GetSmallStraightScore(diceValues);
                break;
            case 12: // Large Straight
                score = GetLargeStraightScore(diceValues);
                break;
            case 9: // Yahtzee
                score = GetYahtzeeScore(diceValues);
                break;
            case 13: // Chance
                score = GetChanceScore(diceValues);
                break;
        }

        scores.Add(score);
    }

    // ÊÚÏÇÏ ÊÇÓåÇ?? ˜å ÏÇÑÇ? ?˜ ãÞÏÇÑ ÎÇÕ åÓÊäÏ ÑÇ ÈÑã?ÑÏÇäÏ
    private int CountDiceWithValue(int[] diceValues, int value)
    {
        int count = 0;
        foreach (int diceValue in diceValues)
        {
            if (diceValue == value)
            {
                count++;
            }
        }
        return count * value;
    }

    // ÇãÊ?ÇÒ ?˜ ÏÓÊå N ÇÒ ?˜ äæÚ (ãËá Three of a Kind ?Ç Four of a Kind)
    private int GetNOfAKindScore(int[] diceValues, int n)
    {
        Array.Sort(diceValues);
        for (int i = 0; i <= diceValues.Length - n; i++)
        {
            if (diceValues[i] == diceValues[i + n - 1])
            {
                return SumDiceValues(diceValues);
            }
        }
        return 0;
    }

    // ÇãÊ?ÇÒ Full House
    private int GetFullHouseScore(int[] diceValues)
    {
        Array.Sort(diceValues);
        if ((diceValues[0] == diceValues[1] && diceValues[2] == diceValues[4]) ||
            (diceValues[0] == diceValues[2] && diceValues[3] == diceValues[4]))
        {
            return 25;
        }
        return 0;
    }

    // ÇãÊ?ÇÒ Small Straight
    private int GetSmallStraightScore(int[] diceValues)
    {
        Array.Sort(diceValues);
        for (int i = 0; i <= diceValues.Length - 4; i++)
        {
            if (diceValues[i] == diceValues[i + 1] - 1 &&
                diceValues[i + 1] == diceValues[i + 2] - 1 &&
                diceValues[i + 2] == diceValues[i + 3] - 1)
            {
                return 30;
            }
        }
        return 0;
    }

    // ÇãÊ?ÇÒ Large Straight
    private int GetLargeStraightScore(int[] diceValues)
    {
        Array.Sort(diceValues);
        for (int i = 0; i <= diceValues.Length - 5; i++)
        {
            if (diceValues[i] == diceValues[i + 1] - 1 &&
                diceValues[i + 1] == diceValues[i + 2] - 1 &&
                diceValues[i + 2] == diceValues[i + 3] - 1 &&
                diceValues[i + 3] == diceValues[i + 4] - 1)
            {
                return 40;
            }
        }
        return 0;
    }

    // ÇãÊ?ÇÒ Yahtzee
    private int GetYahtzeeScore(int[] diceValues)
    {
        for (int i = 0; i <= diceValues.Length - 5; i++)
        {
            if (diceValues[i] == diceValues[i + 1] &&
                diceValues[i + 1] == diceValues[i + 2] &&
                diceValues[i + 2] == diceValues[i + 3] &&
                diceValues[i + 3] == diceValues[i + 4])
            {
                return 50;
            }
        }
        return 0;
    }

    // ÇãÊ?ÇÒ Chance
    private int GetChanceScore(int[] diceValues)
    {
        return SumDiceValues(diceValues);
    }

    // ÌãÚ ãÞÇÏ?Ñ ÊÇÓåÇ
    private int SumDiceValues(int[] diceValues)
    {
        int sum = 0;
        foreach (int value in diceValues)
        {
            sum += value;
        }
        return sum;
    }
}

