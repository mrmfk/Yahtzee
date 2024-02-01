using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GlobalGameManager : NetworkBehaviour
{
    public NetworkVariable<bool> myTurn  = new NetworkVariable<bool>(true , NetworkVariableReadPermission.Everyone , NetworkVariableWritePermission.Owner );

    [ServerRpc(RequireOwnership =false)]
    public void SwitchTurnServerRpc ()
    {
        myTurn.Value = !myTurn.Value;
    }
}
