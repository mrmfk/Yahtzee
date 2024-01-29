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
    public List<DiceAnimation> diceList;
    public bool turnp1=true;
    public int buttonIndexGlob = -1;
    public Text[] p2Scores;
    public Text[] p1Scores;
    public Text labelTurn;
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
       // StartRound();
    }

    public void OnCategoryButtonClick(Button button)
    {
        foreach (Button b in categoryButtons) {
            b.GetComponent<Image>().color = Color.white;
        }

        if (turnp1)
        {
            int buttonIndex = System.Array.IndexOf(categoryButtons, button);
            if (!players[0].scoreChooce[buttonIndex])
            {
                categoryButtons[buttonIndex].GetComponent<Image>().color = Color.red;
                selectedCategory = buttonIndex + 1;
                buttonIndexGlob = buttonIndex;
            }
            else { Debug.Log("select other one"); }

        }
        else
        {
                int buttonIndex = System.Array.IndexOf(categoryButtons, button);
                if (!players[1].scoreChooce[buttonIndex])
                {
                    categoryButtons[buttonIndex].GetComponent<Image>().color = Color.red;
                    selectedCategory = buttonIndex + 1;
                    buttonIndexGlob = buttonIndex;
            }
                else { Debug.Log("select other one"); }

            
        }
        
    }

    public void OnSubmitButtonClick()
    {
        if (buttonIndexGlob != -1 && diceObjects[0].GetComponent<Image>().name!="UIMask" && diceObjects[0].active)
        {

            labelUp();
            rollClick = 0;
            rollButton.gameObject.SetActive(true);
            Player currentPlayer;
            if (turnp1)
                currentPlayer = players[0];
            else
                currentPlayer = players[1];

            
            // ���?����? �� ���?�� �� ���� ����? ������ ���
            currentPlayer.ScoreInCategory(selectedCategory, GetDiceValues(diceList),currentPlayer);
            // updateScores();
            // ����� ��� � ����� �� ��� ���? ?� ��?�� ���?...
            //ScoreRound();
            if (turnp1)
            {
                p1Scores[selectedCategory-1].text = players[0].scores[selectedCategory-1].ToString();
                p1Scores[13].text = players[0].totalScore.ToString();

            }
            else
            {

                p2Scores[selectedCategory-1].text = players[1].scores[selectedCategory-1].ToString();
                p2Scores[13].text = players[1].totalScore.ToString();


            }
            currentRound++;
            buttonIndexGlob = -1;
            turnp1 = !turnp1;
            resetAlldice();
            if (currentRound <= 26) // 13 ��� �� ���? Yahtzee
            {
                // StartRound();
                Debug.Log("Round " + currentRound);
                Debug.Log("p1  " + players[0].totalScore);
                Debug.Log("p2  " + players[1].totalScore);
            }
            else
            {
                EndGame();
            }
        }
        else { Debug.Log("choose category first"); }
    }
    public void updateScores()
    {
        for (int i = 0; i < 13; i++)
        {
           p1Scores[i].text= players[1].scores[i].ToString();
           p2Scores[i].text= players[0].scores[i].ToString();
        }

        p1Scores[13].text = players[1].totalScore.ToString();
        p2Scores[13].text = players[0].totalScore.ToString();


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
        
            // ������ ����? ���?����?
           // int selectedCategory = GetSelectedCategoryFromPlayer(player);

            // ���?����? �� ���?�� �� ���� ����? ������ ���
      /*  if(turnp1)
            players[0].ScoreInCategory(selectedCategory, GetDiceValues(diceList));
        else
            players[1].ScoreInCategory(selectedCategory, GetDiceValues(diceList));*/


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
    int[] GetDiceValues(List<DiceAnimation> dice)
    {
        int[] values = new int[5];
        for (int i = 0; i < 5; i++)
        {
            values[i] = dice[i].faceValue+1;
        }
        return values;
    }
    public void rollBu()
    {
        allDices();
        if (rollClick >= 2) { rollButton.gameObject.SetActive(false); }
        rollClick++;
    }
    public void resetAlldice()
    {
        foreach (Button b in categoryButtons)
        {
            b.GetComponent<Image>().color = Color.white;
        }

        foreach (GameObject l in lockObjects)
        {
            if (l.active)
                diceList[lockObjects.IndexOf(l)].isLocked = !diceList[lockObjects.IndexOf(l)].isLocked;

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
    public void labelUp()
    {
        if (labelTurn.text == "PLAYER 1's TURN" ) { labelTurn.text = "PLAYER 2's TURN"; }
        else { labelTurn.text = "PLAYER 1's TURN"; }

    }

}
