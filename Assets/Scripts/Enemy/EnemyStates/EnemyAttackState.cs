using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.animator.SetTrigger("Attack");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);

        if (distToPlayer > 3.5f /*enemy.attackRange*/ && !enemy.enemyScript.infiniteChase)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        else if (distToPlayer > enemy.attackRange && enemy.enemyScript.infiniteChase)
        {
            enemy.SwitchState(enemy.infiniteChaseState);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }

    public override void OnTriggerStay2D(EnemyStateManager enemy, Collider2D collision)
    {

    }

}
