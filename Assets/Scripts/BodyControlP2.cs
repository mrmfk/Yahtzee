using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BodyControlP2 : NetworkBehaviour
{
    public PlayerController playerControl;
    Vector2 camOffset = new Vector2(0 , 0 ); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
    }

     void FaceMouse()
    {
        transform.right = -(playerControl.direction + camOffset) ;
    }
}
