using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggravation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;
    
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }

        void ChasePlayer()
        {
            if(transform.position.x < player.position.x)
            {
                //Enemy is to the left side of the player so move right
                rb2D.velocity = new Vector2(moveSpeed, 0);
                //Flip player
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                //Enemy is to the right side of the player so move right
                rb2D.velocity = new Vector2(-moveSpeed, 0);
                //Flip player
                transform.localScale = new Vector2(-1, 1);
            }
        }

        void StopChasingPlayer()
        {
            rb2D.velocity = Vector2.zero;
        }
    }
}
