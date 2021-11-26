using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    public GameObject player;
    Player pl;
    public int damage = 10;

    private void Start()
    {
          pl = player.GetComponent<Player>();
    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //Debug.Log("I touched the player");
            pl.TakeDamage(damage);

        }
    }
}
