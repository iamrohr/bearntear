using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.animator.SetTrigger("Idle");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);

        if (distToPlayer > enemy.agroRange)
        {
            enemy.SwitchState(enemy.PatrolState, 3);
        }
        if (distToPlayer < enemy.agroRange)
        {
            enemy.SwitchState(enemy.ChaseState, enemy.reactionTime);
        }

    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}


