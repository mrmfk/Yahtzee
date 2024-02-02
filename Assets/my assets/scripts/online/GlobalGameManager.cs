using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GlobalGameManager : NetworkBehaviour
{
    public NetworkVariable<bool> myTurn = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    /*private static NetworkList<int> scores1;
    private static NetworkList<int> scores2;
    public NetworkList<int> firstPScore = scores1;
    public NetworkList<int> secondPScore = scores2;

    private void Awake()
    {
        scores1 = new NetworkList<int>();
        scores2 = new NetworkList<int>();
        for(int i = 0; i < 13; i++)
        {
            scores1.Add(0);
            scores2.Add(0);
        }

    }



    [ServerRpc(RequireOwnership = false)]
    public void setScores1ServerRpc(int index,int value)
    {
        scores1.Insert(index, value);
    }


    [ServerRpc(RequireOwnership = false)]
    public void setScores2ServerRpc(int index, int value)
    {
        scores2.Insert(index, value);
    }*/

[ServerRpc(RequireOwnership = false)]
public void SwitchTurnServerRpc()
{
    myTurn.Value = !myTurn.Value;
}
}
