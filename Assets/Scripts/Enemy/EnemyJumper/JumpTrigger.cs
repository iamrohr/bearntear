using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public GameObject player;
    public EnemyFollowPath enemyFollowPath;
    public GameObject enemyToJump;
    private SpriteRenderer enemySprite;

    private void Start()
    {
       enemySprite = enemyToJump.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("GameObject1 collided with " + other.name);

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Jaharrrrr I will Jump on you");
            enemyFollowPath.coroutineAllowed = true;
            enemySprite.enabled = true;
        }
    }
}
