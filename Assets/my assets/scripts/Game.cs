using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Player> players;
    public int currentRound;

    void Start()
    {
      
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
            player.RollDice(FindObjectsOfType<Dice>());
        }
    }

    public void EndRound()
    {
        foreach (Player player in players)
        {
 
            player.CalculateTotalScore();
        }

        currentRound++;
        StartRound();
    }
}
