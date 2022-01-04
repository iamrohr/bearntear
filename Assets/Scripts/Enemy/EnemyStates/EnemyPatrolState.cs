using System;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {

        //enemy.attackBox.SetActive(false);
        enemy.animator.SetTrigger("Walk");
        enemy.newEnemyPosition = enemy.EnemyRandPos(3);

        Vector2 dir = enemy.newEnemyPosition - (Vector2)enemy.transform.position;
        enemy.rbHolder.velocity = dir.normalized * enemy.moveSpeed;

        enemy.Invoke(nameof(enemy.StopWalking), 2);

        if (enemy.transform.position.x < enemy.newEnemyPosition.x)
        {
            enemy.transform.localScale = new Vector2(1, 1);
        }
        else
            enemy.transform.localScale = new Vector2(-1, 1);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.enemyScript.infiniteChase)
        {
            enemy.SwitchState(enemy.infiniteChaseState);
        }

        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distToPlayer < enemy.agroRange)
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }

    public override void OnTriggerStay2D(EnemyStateManager enemy, Collider2D collision)
    {

    }

}
