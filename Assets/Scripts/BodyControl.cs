using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BodyControl : NetworkBehaviour
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
        // backToDefault();
        FaceMouse();
        // if(!arrowControl.hasShoot)
        // {

        // }
        // if(arrowControl.hasShoot)
        // {
        //     backToDefault();
        // }
    }

     void FaceMouse()
    {
        transform.right = playerControl.direction + camOffset ;
    }

    void backToDefault ()
    {
     transform.Rotate(0,0,0);
    }
}
