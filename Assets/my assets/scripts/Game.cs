using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Player> players;
    public int currentRound;

    void Start()
    {
        // ‘—Ê⁄ »«“? »«  ⁄œ«œ »«“?ò‰«‰ „‘Œ’
        StartGame(2);
    }

    void StartGame(int playerCount)
    {
        players = new List<Player>();
        for (int i = 0; i < playerCount; i++)
        {
            players.Add(new Player("Player " + (i + 1)));
        }

        currentRound = 1;
        StartRound();
    }

    void StartRound()
    {
        Debug.Log("Round " + currentRound);

        foreach (Player player in players)
        {
            Dice[] dice = FindObjectsOfType<Dice>();
            player.RollDice(dice);

            // ‰„«?‘ ‰ «?Ã Å” «“ Å— «»  «”ùÂ«
            Debug.Log(player.playerName + "'s Dice Values: " + GetDiceValues(dice));
            Debug.Log(player.playerName + "'s Scoresheet:");
            DisplayScoreSheet(player);
        }

        // „—Õ·Â «„ ?«“œÂ?
        ScoreRound();
    }

    public void ScoreRound()
    {
        foreach (Player player in players)
        {
            // «‰ Œ«» œ” Âù? «„ ?«“œÂ?
            int selectedCategory = GetSelectedCategoryFromPlayer(player);

            // «„ ?«“œÂ? »Â »«“?ò‰ »— «”«” œ” Âù? «‰ Œ«» ‘œÂ
            player.ScoreInCategory(selectedCategory, GetDiceValues(FindObjectsOfType<Dice>()));
        }

        // « „«„ œÊ— Ê «œ«„Â »Â œÊ— »⁄œ? ?« Å«?«‰ »«“?...
        currentRound++;
        if (currentRound <= 13) // 13 œÊ— œ— »«“? Yahtzee
        {
            StartRound();
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // «‰Ã«„ ⁄„·?«  Å«?«‰ »«“?
        Debug.Log("Game Over");

        // ‰„«?‘ ‰ «?Ã ‰Â«??
        foreach (Player player in players)
        {
            Debug.Log(player.playerName + "'s Final Score: " + player.totalScore);
        }
    }

    // «‰ Œ«» œ” Âù? «„ ?«“œÂ? «“ »«“?ò‰
    public int GetSelectedCategoryFromPlayer(Player player)
    {
        return Random.Range(1, 14);
    }

    // ‰„«?‘ ‰ «?Ã «„ ?«“œÂ? »—«? ?ò »«“?ò‰
    void DisplayScoreSheet(Player player)
    {
        foreach (int score in player.scores)
        {
            Debug.Log(score);
        }
        Debug.Log("Total Score: " + player.totalScore);
    }

    // ê—› ‰ „ﬁ«œ?—  «”ùÂ«
    int[] GetDiceValues(Dice[] dice)
    {
        int[] values = new int[dice.Length];
        for (int i = 0; i < dice.Length; i++)
        {
            values[i] = dice[i].faceValue;
        }
        return values;
    }
}
