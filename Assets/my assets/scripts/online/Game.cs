using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
public class Game : NetworkBehaviour
{
    [SerializeField] GameObject turnImage;
    [SerializeField] GlobalGameManager globalGameManager;

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
    public Text[] p1Scores;
    public Text[] p2Scores;
    public Text labelTurn;
    void Start()
    {
        // StartGame(2);
    }
    private void Update() 
    {
        if(IsHost && globalGameManager.myTurn.Value)
        {
            turnImage.SetActive(false);
            
        }
        else if(!IsHost && !globalGameManager.myTurn.Value)
        {
            turnImage.SetActive(false);
            
        }
        else {
            turnImage.SetActive(true);
        }
    }

    public void OnClickTurn()
    {
        Debug.Log("Turn CHanged");
        globalGameManager.SwitchTurnServerRpc();
    }

    // void StartGame(int playerCount)
    // {
    //     players = new List<Player>();
    //     for (int i = 0; i < playerCount; i++)
    //     {
    //         players.Add(new Player("Player " + (i + 1)));
    //     }
    //     currentRound = 1;
    //    // StartRound();
    // }
    // public void OnCategoryButtonClick(Button button)
    // {

    //     int buttonIndex = System.Array.IndexOf(categoryButtons, button);
    //     diceColorReset(turnp1);

    //     if (turnp1)
    //     {
    //         if (!players[0].scoreChooce[buttonIndex])
    //         {
    //             categoryButtons[buttonIndex].GetComponent<Image>().color = Color.red;
    //             selectedCategory = buttonIndex + 1;
    //             buttonIndexGlob = buttonIndex;
    //         }
    //         else { Debug.Log("select other one"); }

    //     }
    //     else
    //     {
    //             if (!players[1].scoreChooce[buttonIndex])
    //             {
    //                 categoryButtons[buttonIndex].GetComponent<Image>().color = Color.red;
    //                 selectedCategory = buttonIndex + 1;
    //                 buttonIndexGlob = buttonIndex;
    //         }
    //             else { Debug.Log("select other one"); }

            
    //     }
        
    // }
    // public void OnSubmitButtonClick()
    // {
    //     if (buttonIndexGlob != -1 && diceObjects[0].GetComponent<Image>().name!="UIMask" && diceObjects[0].active)
    //     {

    //         labelUp();
    //         rollClick = 0;
    //         rollButton.gameObject.SetActive(true);
    //         Player currentPlayer;
    //         if (turnp1)
    //             currentPlayer = players[0];
    //         else
    //             currentPlayer = players[1];

        
    //         currentPlayer.ScoreInCategory(selectedCategory, GetDiceValues(diceList),currentPlayer);
           
    //         if (turnp1)
    //         {
    //             p1Scores[selectedCategory-1].text = players[0].scores[selectedCategory-1].ToString();
    //             p1Scores[13].text = players[0].totalScore.ToString();

    //         }
    //         else
    //         {

    //             p2Scores[selectedCategory-1].text = players[1].scores[selectedCategory-1].ToString();
    //             p2Scores[13].text = players[1].totalScore.ToString();


    //         }
    //         currentRound++;
    //         buttonIndexGlob = -1;

    //         turnp1 = !turnp1;
    //         resetAlldice();
    //         if (currentRound <= 26) 
    //         {
    //             // StartRound();
    //             Debug.Log("Round " + currentRound);
    //             Debug.Log("p1  " + players[0].totalScore);
    //             Debug.Log("p2  " + players[1].totalScore);
    //         }
    //         else
    //         {
    //             EndGame();
    //         }
    //     }
    //     else { Debug.Log("choose category first"); }
    // }
    // void EndGame()
    // {
    //     // ����� ���?�� ��?�� ���?
    //     Debug.Log("Game Over");

    //     // ���?� ���?� ���??
    //     foreach (Player player in players)
    //     {
    //         Debug.Log(player.playerName + "'s Final Score: " + player.totalScore);
    //     }
    // }
    // int[] GetDiceValues(List<DiceAnimation> dice)
    // {
    //     int[] values = new int[5];
    //     for (int i = 0; i < 5; i++)
    //     {
    //         values[i] = dice[i].faceValue+1;
    //     }
    //     return values;
    // }
    // public void rollBu()
    // {
    //     allDices();
    //     if (rollClick >= 2) { rollButton.gameObject.SetActive(false); }
    //     rollClick++;
    // }
    // public void resetAlldice()
    // {
    //     diceColorReset(turnp1);
 
    //     foreach (GameObject l in lockObjects)
    //     {
    //         if (l.active)
    //             diceList[lockObjects.IndexOf(l)].isLocked = !diceList[lockObjects.IndexOf(l)].isLocked;

    //         l.SetActive(false);
    //     }
    //  foreach(GameObject D in diceObjects)
    //     {
    //         D.SetActive(false);
    //     }

    
    // }
    // public void allDices()
    // {
    //     foreach (GameObject D in diceObjects)
    //     {
    //         D.SetActive(true);
    //     }

    // }
    // public void diceColorReset(bool trn)
    // {
    //     if (trn)
    //     {
    //         for (int i = 0; i < 13; i++)
    //         {
    //             if (players[0].scoreChooce[i])
    //                 categoryButtons[i].GetComponent<Image>().color = Color.yellow;
    //             else
    //                 categoryButtons[i].GetComponent<Image>().color = Color.white;
    //         }
    //     }
    //     else
    //     {
    //         for (int i = 0; i < 13; i++)
    //         {
    //             if (players[1].scoreChooce[i])
    //                 categoryButtons[i].GetComponent<Image>().color = Color.yellow;
    //             else
    //                 categoryButtons[i].GetComponent<Image>().color = Color.white;
    //         }
    //     }

    // }
    // public void labelUp()
    // {
    //     if (labelTurn.text == "PLAYER 1's TURN" ) { labelTurn.text = "PLAYER 2's TURN"; }
    //     else { labelTurn.text = "PLAYER 1's TURN"; }

    // }

}
