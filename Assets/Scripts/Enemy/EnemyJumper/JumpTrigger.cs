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
        if (other.gameObject.tag == "Player")
        {
            enemyFollowPath.coroutineAllowed = true;
            enemySprite.enabled = true;
            Destroy(gameObject, 2f);
        }
    }
}
