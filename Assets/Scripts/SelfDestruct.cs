using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
   [SerializeField] float selfDestructTime ;
   private float timer;
    void Start()
    {
        timer = selfDestructTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        } 
    }
}
