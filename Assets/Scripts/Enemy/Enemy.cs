using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D enemyRB;
    public GameObject cottonDrop;

    public int currentHealth, maxHealth;
    public int dropRange;
    
    public Vector2 enemyPos;

    private void Start()
    {
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        enemyPos = enemyRB.transform.localPosition;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            InstansiateDrop();
            Destroy(this.gameObject);
        }
    }

    private void InstansiateDrop()
    {
        int RandomNumber = Random.Range(1, dropRange + 1);

        if(RandomNumber == dropRange)
        {
            Instantiate(cottonDrop, new Vector2(enemyPos.x, enemyPos.y), Quaternion.identity);
           
        }
        
    }
}
