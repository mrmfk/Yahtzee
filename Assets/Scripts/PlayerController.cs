using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
// using UnityEditor.PackageManager;

public class PlayerController : NetworkBehaviour
{

    Rigidbody2D myRB;
    
    [SerializeField] float maxHealth = 100f;

    [SerializeField] float currentHealth = 100f;
    [SerializeField] NetworkVariable<float> currentHealthnet ;

    


    float headDamage = 40f;

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

    [SerializeField]GameManager  gameManager ;

    public GameObject arrow;

    [SerializeField]Image healthBar;

    

    public bool hasShoot;

    [SerializeField] private Transform bowPosition;

    GameObject containingBow ;



    // public override void OnNetworkSpawn()
    // {
    //     NetworkManager.Singleton.SceneManager.OnLoadComplete += DespawnPlayer;
    // }

    // private void DespawnPlayer(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    // {
        
    //     if(IsOwner && sceneName == "Menu" )
    //     {
    //         Debug.Log("Despawned");
    //         GetComponent<NetworkObject>().Despawn(true);
    //         Destroy(gameObject);
            
    //     }
    // }

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        containingBow = this.gameObject.transform.GetChild(8).gameObject;
        gameManager = FindObjectOfType<GameManager>();

        
    }
    

    // Update is called once per frame
    void Update()
    {
        
         Debug.Log("Client : "+ OwnerClientId + "Turn iS : " + gameManager.myTurn.Value );

        if(!IsOwner){return;}
         ChangeHealthBarClientRpc(currentHealth);


        if ( (int)OwnerClientId == gameManager.myTurn.Value )
        {
            
            if(!gameManager.endMatchNet.Value && !gameManager.pauseClicked)
            {

                shoot();
            }

        }



         direction = dragStartPos - draggingPos ;

         FaceMouse();

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        Debug.Log(other.otherCollider.name);
        if(other.gameObject.tag == "Arrow" )
        {
            if (other.otherCollider.name == "Head")
            {
                TakeDamage(other.gameObject.GetComponent<Arrow>().arrowDamage + headDamage);
                Debug.Log(other.gameObject.GetComponent<Arrow>().arrowDamage + headDamage);
            }
            else
            {
                TakeDamage(other.gameObject.GetComponent<Arrow>().arrowDamage);
                Debug.Log(other.gameObject.GetComponent<Arrow>().arrowDamage);

            }

            // MakeArroChild(other.gameObject ,other.otherCollider.name );
        }
    }
    
    void TakeDamage (float amount) 
    {
       
        currentHealth -= amount;
        ChangeHealthBarClientRpc(currentHealth);
        Debug.Log(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die ()
    {
        gameManager.ChangeScoreClientRpc(OwnerClientId); 
        gameManager.EndMatchClientRpc(true);

 
        Destroy(gameObject);
    }



    [ClientRpc]
    void ChangeHealthBarClientRpc(float currentHealth)
    {
        // healthBar.fillAmount = currentHealth / maxHealth;
        currentHealthnet =  new NetworkVariable<float>(currentHealth);
        
        healthBar.fillAmount = currentHealthnet.Value/100;
        
    }

    void MakeArroChild (GameObject arrow , string collided) 
    {
        if(collided == "Left Leg" || collided == "Right Leg")
        {
            arrow.transform.parent = this.gameObject.transform;
        }
        else 
        {
            arrow.transform.parent = this.gameObject.transform.GetChild(8).gameObject.transform; 

        }
        
    }

    
    void shoot()
    {
        if(Input.GetMouseButtonDown(0))
            {
                
                DragStart();
            }

            if(Input.GetMouseButton(0))
            {
                Dragging();
            }

            if(Input.GetMouseButtonUp(0))
            {
                DragRelease();
                hasShoot = true;
                // gameManager.SwitchTurnServerRpc ();
                
             
                
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
    
    void DragRelease()
    {
       
        myLine.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + camOffset;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force , maxDrag) * arrowSpeed;
        // Debug.Log("force is : " + force);
        // Debug.Log("clapmed force is : " + clampedForce);
        Vector3 noforce = new Vector3(0 , 0 ,0 );
        if(!force.Equals(noforce)  )
        {

            shoottServerRpc(clampedForce);
            gameManager.SwitchTurnServerRpc ();
        }
       
        // gameManager.sh();
    }

    [ServerRpc]
    void shoottServerRpc(Vector3 clampedForce)
    {
        GameObject arrowIns = Instantiate(arrow, bowPosition.position ,transform.rotation );
        arrowIns.GetComponent<NetworkObject>().Spawn(true);

        arrowIns.GetComponent<Rigidbody2D>().AddForce(clampedForce,ForceMode2D.Impulse);

    }



     void FaceMouse()
    {
        if(transform.localScale.x == -1)
        {

            containingBow.transform.right = -(direction ) ;
        }
        else
        {
            containingBow.transform.right = direction  ;
        }
    }


    
}
