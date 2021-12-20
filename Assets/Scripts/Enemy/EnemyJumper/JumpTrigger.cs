using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public GameObject enemyJumper;
    //private EnemyFollowPath enemyFollowPath;

    [Header("Components")]
    [HideInInspector] public GameObject player;

    private void Start()
    { 
     //enemyFollowPath = enemyJumper.GetComponent<EnemyFollowPath>();
     //player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("GameObject1 collided with " + other.name);

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Jaharrrrr I will Jump on you");
            enemyJumper.SetActive(true);
        }
    }
}