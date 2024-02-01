using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    public string playerName;
    public List<int> scores; 
    public List<bool> scoreChooce; 
    public int totalScore; 
    public bool[] diceLocked; 

    // public Player(string name)
    // {
    //     playerName = name;
    //     scores = new List<int>();
    //     scoreChooce = new List<bool>();
    //     diceLocked = new bool[5];
    //     for (int i = 0; i < 13; i++)
    //     {
    //         scores.Add(0);
    //         scoreChooce.Add(false);
    //     }
    // }



    // public void ToggleDiceLock(int index)
    // {
    //     diceLocked[index] = !diceLocked[index];
    //     Debug.Log(playerName + "'s die " + (index + 1) + " is " + (diceLocked[index] ? "locked" : "unlocked"));
    // }




    // public void ScoreInCategory(int category, int[] diceValues,Player p)
    // {
    //     int score = 0;

    //     switch (category)
    //     {
    //         case 1:
    //         case 2:
    //         case 3:
    //         case 4:
    //         case 5:
    //         case 6:
    //             score = CountDiceWithValue(diceValues, category);
    //             break;
    //         case 7: // Three of a Kind
    //             score = GetNOfAKindScore(diceValues, 3);
    //             break;
    //         case 8: // Four of a Kind
    //             score = GetNOfAKindScore(diceValues, 4);
    //             break;
    //         case 9: // Yahtzee
    //             score = GetYahtzeeScore(diceValues);
    //             break;
    //         case 10: // Full House
    //             score = GetFullHouseScore(diceValues);
    //             break;
    //         case 11: // Small Straight
    //             score = GetSmallStraightScore(diceValues);
    //             break;
    //         case 12: // Large Straight
    //             score = GetLargeStraightScore(diceValues);
    //             break;
    //         case 13: // Chance
    //             score = GetChanceScore(diceValues);
    //             break;
    //     }

    //     p.scores[category-1]=score;
    //     p.scoreChooce[category-1]=true;
    //     p.totalScore += score;
    // }


    // private int CountDiceWithValue(int[] diceValues, int value)
    // {
    //     int count = 0;
    //     foreach (int diceValue in diceValues)
    //     {
    //         if (diceValue == value)
    //         {
    //             count++;
    //         }
    //     }
    //     return count * value;
    // }


    // private int GetNOfAKindScore(int[] diceValues, int n)
    // {
    //     Array.Sort(diceValues);
    //     for (int i = 0; i <= diceValues.Length - n; i++)
    //     {
    //         if (diceValues[i] == diceValues[i + n - 1])
    //         {
    //             return SumDiceValues(diceValues);
    //         }
    //     }
    //     return 0;
    // }


    // private int GetFullHouseScore(int[] diceValues)
    // {
    //     Array.Sort(diceValues);
    //     if ((diceValues[0] == diceValues[1] && diceValues[2] == diceValues[4]) ||
    //         (diceValues[0] == diceValues[2] && diceValues[3] == diceValues[4]))
    //     {
    //         return 25;
    //     }
    //     return 0;
    // }

    // private int GetSmallStraightScore(int[] diceValues)
    // {
    //     Array.Sort(diceValues);
    //     int[] count = new int[6];
    //     for (int i = 0; i < diceValues.Length; i++)
    //     {
    //         count[diceValues[i] - 1]++;
    //     }

    //     for (int i = 0; i <= count.Length - 4; i++)
    //     {
    //         if (count[i] > 0 && count[i + 1] > 0 && count[i + 2] > 0 && count[i + 3] > 0)
    //         {
    //             return 30;
    //         }
    //     }

    //     return 0;
    // }



    // private int GetLargeStraightScore(int[] diceValues)
    // {
    //     Array.Sort(diceValues);
    //     for (int i = 0; i <= diceValues.Length - 5; i++)
    //     {
    //         if (diceValues[i] == diceValues[i + 1] - 1 &&
    //             diceValues[i + 1] == diceValues[i + 2] - 1 &&
    //             diceValues[i + 2] == diceValues[i + 3] - 1 &&
    //             diceValues[i + 3] == diceValues[i + 4] - 1)
    //         {
    //             return 40;
    //         }
    //     }
    //     return 0;
    // }


    // private int GetYahtzeeScore(int[] diceValues)
    // {
    //     for (int i = 0; i <= diceValues.Length - 5; i++)
    //     {
    //         if (diceValues[i] == diceValues[i + 1] &&
    //             diceValues[i + 1] == diceValues[i + 2] &&
    //             diceValues[i + 2] == diceValues[i + 3] &&
    //             diceValues[i + 3] == diceValues[i + 4])
    //         {
    //             return 50;
    //         }
    //     }
    //     return 0;
    // }


    // private int GetChanceScore(int[] diceValues)
    // {
    //     return SumDiceValues(diceValues);
    // }


    // private int SumDiceValues(int[] diceValues)
    // {
    //     int sum = 0;
    //     foreach (int value in diceValues)
    //     {
    //         sum += value;
    //     }
    //     return sum;
    // }
}

