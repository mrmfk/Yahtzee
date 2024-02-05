using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
public class Game : NetworkBehaviour
{
    [SerializeField] GameObject turnImage;
    [SerializeField] GlobalGameManager globalGameManager;

    public List<Player> players;
    public Player player1;
    public Player player2;
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
   public ConnectController controller;
    public Text rollText;
    void Start()
    {
         StartGame(2);
    }
    private void Update() 
    {
        if(IsHost && globalGameManager.myTurn.Value)
        {
            turnImage.SetActive(false);
            if (globalGameManager.changeHost.Value)
            {
                players[1].scores[globalGameManager.catGlob.Value] = globalGameManager.scoreGlob.Value;
                players[1].scoreChooce[globalGameManager.catGlob.Value] = true;
                players[1].totalScore += globalGameManager.scoreGlob.Value;
                globalGameManager.SwitchP1BoolServerRpc();
            }
            
        }
        else if(!IsHost && !globalGameManager.myTurn.Value)
        {
            turnImage.SetActive(false);
            
        }
        else {
            turnImage.SetActive(true);
        }
        updateScoresTable();
    }

    public void OnClickTurn()
    {
      controller.popUpTextInit(Color.black, "wait for yor turn...");
        globalGameManager.SwitchTurnServerRpc();
    }

    void StartGame(int playerCount)
    {
        players = new List<Player>();
        players.Add(player1);
        players.Add(player2);
        player1.createPlayer("Host");
        player2.createPlayer("client");
      currentRound = 1;
    }
    public void OnCategoryButtonClick(Button button)
    {

        int buttonIndex = System.Array.IndexOf(categoryButtons, button);
        diceColorReset(turnp1);

        if (turnp1)
        {
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
            if (!players[1].scoreChooce[buttonIndex])
            {
                categoryButtons[buttonIndex].GetComponent<Image>().color = Color.red;
                selectedCategory = buttonIndex + 1;
                buttonIndexGlob = buttonIndex;
            }
            else { Debug.Log("select other one"); }


        }

    }

    [ClientRpc]
    public void setScoresClientRpc(int score, int cat)
    {
        if (!IsHost)
        {
            players[0].scores[cat] = score;
            players[0].scoreChooce[cat] = true;
            players[0].totalScore += score;
        }
    }
    public void OnSubmitButtonClick()
    {
        if (buttonIndexGlob != -1 && diceObjects[0].GetComponent<Image>().name != "UIMask" && diceObjects[0].active)
        {
            rollText.text = "ROLL(+++)";
            rollClick = 0;
            rollButton.gameObject.SetActive(true);

            if (IsHost)
            {
                players[0].ScoreInCategory(selectedCategory, GetDiceValues(diceList), players[0]);
               
                
                // globalGameManager.catGlob.Value = selectedCategory - 1;
              //  globalGameManager.scoreGlob.Value = players[0].scores[selectedCategory - 1];
            }
            else
            {

                players[1].ScoreInCategory(selectedCategory, GetDiceValues(diceList), players[1]);
                
                //globalGameManager.catGlob.Value = selectedCategory - 1;
                //globalGameManager.catGlob.Set() = selectedCategory - 1;
               //globalGameManager.scoreGlob.Value = players[1].scores[selectedCategory - 1];
            }
            if (IsHost)
            {
                setScoresClientRpc(globalGameManager.scoreGlob.Value, globalGameManager.catGlob.Value);
            }
            else
            {
/*
                players[1].scores[globalGameManager.catGlob.Value] = globalGameManager.scoreGlob.Value;
                players[1].scoreChooce[globalGameManager.catGlob.Value] = true;
                players[1].totalScore += globalGameManager.scoreGlob.Value;
*/
                //globalGameManager.setScore1ServerRpc(globalGameManager.scoreGlob.Value);
                //globalGameManager.setScore2ServerRpc(globalGameManager.catGlob.Value);

                //globalGameManager.setScoresServerRpc(globalGameManager.scoreGlob.Value, globalGameManager.catGlob.Value, players[1]);

            }
            currentRound++;
            buttonIndexGlob = -1;
            OnClickTurn();
            turnp1 = !turnp1;
            resetAlldice();
            if (currentRound <= 13)
            {
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
    void EndGame()
    {
        Debug.Log("Game Over");
        foreach (Player player in players)
        {
            Debug.Log(player.playerName + "'s Final Score: " + player.totalScore);
        }
    }

    public void updateScoresTable()
    {
        for(int i = 0; i < 13; i++)
        {
            p2Scores[i].text = players[1].scores[i].ToString();
            p1Scores[i].text = players[0].scores[i].ToString();
            

        }
        p2Scores[13].text = players[1].totalScore.ToString();
        p1Scores[13].text = players[0].totalScore.ToString();

    }
    int[] GetDiceValues(List<DiceAnimation> dice)
    {
        int[] values = new int[5];
        for (int i = 0; i < 5; i++)
        {
            values[i] = dice[i].faceValue + 1;
        }
        return values;
    }
    public void rollBu()
    {
        allDices();
        if (rollClick >= 2)
        {
            rollText.text = "ROLL(+++)";
            rollButton.gameObject.SetActive(false);
        }
        rollClick++;
        if (rollClick == 1)
            rollText.text = "ROLL(++)";
        if (rollClick == 2)
            rollText.text = "ROLL(+)";
    }
    public void resetAlldice()
    {
        diceColorReset(!turnp1);

        foreach (GameObject l in lockObjects)
        {
            if (l.active)
                diceList[lockObjects.IndexOf(l)].isLocked = !diceList[lockObjects.IndexOf(l)].isLocked;

            l.SetActive(false);
        }
        foreach (GameObject D in diceObjects)
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
    public void diceColorReset(bool trn)
    {
        if (!trn)
        {
            for (int i = 0; i < 13; i++)
            {
                if (players[0].scoreChooce[i])
                    categoryButtons[i].GetComponent<Image>().color = Color.yellow;
                else
                    categoryButtons[i].GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            for (int i = 0; i < 13; i++)
            {
                if (players[1].scoreChooce[i])
                    categoryButtons[i].GetComponent<Image>().color = Color.yellow;
                else
                    categoryButtons[i].GetComponent<Image>().color = Color.white;
            }
        }

    }
    public void labelUp()
    {
        if (IsHost && globalGameManager.myTurn.Value) { labelTurn.text = "Client's TURN"; }
        else if(IsHost && !globalGameManager.myTurn.Value) { labelTurn.text = "Host's TURN"; }
        else if (globalGameManager.myTurn.Value) { labelTurn.text = "Host's TURN"; }
        else { labelTurn.text = "Client's TURN"; }
         /*  if (labelTurn.text == "Host's TURN") { labelTurn.text = "Client's TURN"; }
        else { labelTurn.text = "Host's TURN"; }*/

    }

}
