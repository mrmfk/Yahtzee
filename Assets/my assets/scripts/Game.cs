using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Player> players;
    public int currentRound;

    void Start()
    {
        // ���� ���? �� ����� ���?���� ����
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

            // ���?� ���?� �� �� ����� ��ӝ��
            Debug.Log(player.playerName + "'s Dice Values: " + GetDiceValues(dice));
            Debug.Log(player.playerName + "'s Scoresheet:");
            DisplayScoreSheet(player);
        }

        // ����� ���?����?
        ScoreRound();
    }

    public void ScoreRound()
    {
        foreach (Player player in players)
        {
            // ������ ����? ���?����?
            int selectedCategory = GetSelectedCategoryFromPlayer(player);

            // ���?����? �� ���?�� �� ���� ����? ������ ���
            player.ScoreInCategory(selectedCategory, GetDiceValues(FindObjectsOfType<Dice>()));
        }

        // ����� ��� � ����� �� ��� ���? ?� ��?�� ���?...
        currentRound++;
        if (currentRound <= 13) // 13 ��� �� ���? Yahtzee
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
        // ����� ���?�� ��?�� ���?
        Debug.Log("Game Over");

        // ���?� ���?� ���??
        foreach (Player player in players)
        {
            Debug.Log(player.playerName + "'s Final Score: " + player.totalScore);
        }
    }

    // ������ ����? ���?����? �� ���?��
    public int GetSelectedCategoryFromPlayer(Player player)
    {
        return Random.Range(1, 14);
    }

    // ���?� ���?� ���?����? ���? ?� ���?��
    void DisplayScoreSheet(Player player)
    {
        foreach (int score in player.scores)
        {
            Debug.Log(score);
        }
        Debug.Log("Total Score: " + player.totalScore);
    }

    // ����� ����?� ��ӝ��
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
