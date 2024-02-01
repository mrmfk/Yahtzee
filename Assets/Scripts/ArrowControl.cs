using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
// using UnityEditor.PackageManager;
// using UnityEditor.PackageManager;

public class ArrowControl : NetworkBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
    [SerializeField] float maxDrag = 5f;
    // [SerializeField] Rigidbody2D arrowRigidBody;
    [SerializeField] LineRenderer myLine;

    Vector3 dragStartPos;
    Touch touch;

    Vector3 camOffset = new Vector3(0 , 0 , 10); 

    public Vector2 direction ;

    Vector2 bowPos ;
    Vector3 draggingPos;

    [SerializeField]GameManager gameManager ;

    public GameObject arrow; 

    private NetworkVariable<int> myTurn  = new NetworkVariable<int>(0 , NetworkVariableReadPermission.Everyone , NetworkVariableWritePermission.Server );

    public bool hasShoot;

    public override void OnNetworkSpawn()
    {
        // gameManager = NetworkObject.FindObjectOfType<GameManager>();
    }
    
    void Start() 
    {
        // gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        
    }

    void Update()
    {
        // Debug.Log("in arrow controll local client id is : " + NetworkManager.Singleton.LocalClientId);
        if(OwnerClientId == 0)
        {
            Debug.Log("Turn iS : " + myTurn.Value+" and the client is : " + OwnerClientId);

        }

        
        // if(!IsOwner){return;}
        // if(Input.GetMouseButtonDown(0))
        //     {
        //         if(IsServer)
        //         {
        //             SwitchTurnServerRpc();

        //         }
        //         // Debug.Log("Client " + NetworkManager.Singleton.LocalClientId + "clicked!!!!!");
        //     }
            
                shootServerRpc();
        // if(OwnerClientId == 0)
        // {

        //     if ( (int)NetworkManager.Singleton.LocalClientId == myTurn.Value )
        //     {

        //     }
        // }

        
    
        direction = dragStartPos - draggingPos ;
        // FaceMouse(direction);

     }

    [ServerRpc(RequireOwnership = false)]
    void shootServerRpc()
    {
        if(Input.GetMouseButtonDown(0))
            {
                // Debug.Log("Client " + NetworkManager.Singleton.LocalClientId + "clicked!!!!!");
                DragStart();
            }

            if(Input.GetMouseButton(0))
            {
                Dragging();
            }

            if(Input.GetMouseButtonUp(0))
            {
                DragReleaseServerRpc();
                hasShoot = true;
                SwitchTurnServerRpc();
            }
    }
    
    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        myLine.positionCount = 2;
        myLine.SetPosition(0,dragStartPos);
        myLine.useWorldSpace = true;
    }
    
    void Dragging()
    {
        draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        myLine.SetPosition(1 , draggingPos);
    }
    [ServerRpc(RequireOwnership = false)]
    void DragReleaseServerRpc()
    {
        GameObject arrowIns = Instantiate(arrow,transform.position ,transform.rotation );
        arrowIns.GetComponent<NetworkObject>().Spawn(true);
        myLine.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + camOffset;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force , maxDrag) * arrowSpeed;
        arrowIns.GetComponent<Rigidbody2D>().AddForce(clampedForce,ForceMode2D.Impulse);
        // gameManager.sh();
    }

    void FaceMouse(Vector2 lookDirection)
    {
        transform.right = lookDirection;
    }

    [ServerRpc]
      public void SwitchTurnServerRpc ()
    {
        
        if(myTurn.Value==0  )
        {
            myTurn.Value = 1;
        }
        else if(myTurn.Value ==1  )
        {
            myTurn.Value = 0;
        }
        
    }

    
}
