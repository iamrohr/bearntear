using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggravation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField]  float attackRange;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float returnSpeed = 5f;
    [SerializeField] GameObject attackBox;
    
    Vector2 startPosition;

    Rigidbody2D playerRb2D;

    void Start()
    {
        playerRb2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        startPosition = new Vector2(transform.position.x, transform.position.y);
        attackBox.SetActive(false);
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        if (distToPlayer < agroRange && distToPlayer < attackRange)
        {
            //do
            //{
                StartCoroutine(AttackPlayer());
            //}
            //while (distToPlayer < attackRange);

        }
        else
        {
            StopChasingPlayer();
        }

        void ChasePlayer()
        {
            float step = moveSpeed * Time.deltaTime;

            if (transform.position.x < player.position.x)
            {
                //Enemy is to the left side of the player so move right
                transform.position = Vector2.MoveTowards(transform.position, playerRb2D.position, step);
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                //Enemy is to the left side of the player so move left
                transform.position = Vector2.MoveTowards(transform.position, playerRb2D.position, step);
                transform.localScale = new Vector2(-1, 1);
            }
        }

        void StopChasingPlayer()
        {
            float step = returnSpeed * Time.deltaTime;

            if (transform.position.x < player.position.x)
            {
                //Flip Player towards the starting position
                transform.position = Vector2.MoveTowards(transform.position, startPosition, step);
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                //Flip Player towards the starting position
                transform.position = Vector2.MoveTowards(transform.position, startPosition, step);
                transform.localScale = new Vector2(1, 1);
            }

        }

        IEnumerator AttackPlayer()
        {

            attackBox.SetActive(true);
            yield return new WaitForSeconds(2f);
            attackBox.SetActive(false);

        }

        void IdleState()
        {

        }
    }
}
