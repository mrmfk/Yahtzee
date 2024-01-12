using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public List<Player> players;
    public int currentRound;
    public Button[] categoryButtons;
    public Button submitButton;
    public Button rollButton;
    int selectedCategory;
    int rollClick=0;
    public List<GameObject> diceObjects;
    public List<GameObject> lockObjects;
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

    public void OnCategoryButtonClick(Button button)
    {

        int buttonIndex = System.Array.IndexOf(categoryButtons, button);

         selectedCategory = buttonIndex + 1;

    }

    public void OnSubmitButtonClick()
    {
        resetAlldice();
        
        rollClick = 0;
       rollButton.gameObject.SetActive(true); 
        Player currentPlayer = players[0];
        // ���?����? �� ���?�� �� ���� ����? ������ ���
        currentPlayer.ScoreInCategory(selectedCategory, GetDiceValues(FindObjectsOfType<Dice>()));
        // ����� ��� � ����� �� ��� ���? ?� ��?�� ���?...
        //ScoreRound();
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
    void StartRound()
    {
        Debug.Log("Round " + currentRound);

        foreach (Player player in players)
        {
            Dice[] dice = FindObjectsOfType<Dice>();
            
            
           // player.RollDice(dice);

         // ���?� ���?� �� �� ����� ��ӝ��
         // Debug.Log(player.playerName + "'s Dice Values: " +GetDiceValues(dice));
         //   Debug.Log(player.playerName + "'s Scoresheet:");
            DisplayScoreSheet(player);
        }

        // ����� ���?����?
        
    }

    public void ScoreRound()
    {
        foreach (Player player in players)
        {
            // ������ ����? ���?����?
           // int selectedCategory = GetSelectedCategoryFromPlayer(player);

            // ���?����? �� ���?�� �� ���� ����? ������ ���
            player.ScoreInCategory(selectedCategory, GetDiceValues(FindObjectsOfType<Dice>()));
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
public void rollBu()
    {
        allDices();
        if (rollClick >= 3) { rollButton.gameObject.SetActive(false); }
        rollClick++;
    }
    public void resetAlldice()
    {

        foreach(GameObject l in lockObjects)
        {
            l.SetActive(false);
        }
     foreach(GameObject D in diceObjects)
        {
            D.SetActive(false);
        }

    }
    public void allDices()
    {
        foreach (GameObject D in diceObjects)
        {
            D.SetActive(true);
        }

    }

}
