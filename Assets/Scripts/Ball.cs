using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private float moveSpeed = 10;
    private float howManyTouchWalls;
    private void Awake()
    {
        
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            howManyTouchWalls++;
            if (howManyTouchWalls > 10)
            {
                
                rigidbody2D.gravityScale = .1f;
            }
        }
       
    }
    void Update()
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * moveSpeed;
        

    }
}
