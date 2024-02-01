using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class Arrow : NetworkBehaviour
{
    [SerializeField] float selfDestructTime ;
    private float timer;
    [SerializeField] public float arrowDamage = 10f;

    bool hasHit= false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = selfDestructTime;
    }

    // Update is called once per frame
    void Update()
    {

        if(hasHit == false)
        {
            trackMovement();
        }

    }

    void trackMovement()
    {
        Vector2 direction = rb.velocity;

        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        hasHit = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        Invoke("destroyArrow" , selfDestructTime );
        
        
    }

    void destroyArrow()
    {
        GetComponent<NetworkObject>().Despawn(true);
        // Destroy(gameObject);
    }
}
