using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);

        if (distToPlayer > enemy.agroRange)
        {
             
        }
        if (distToPlayer < enemy.agroRange && enemy.ReactionTime())
        {
            //Immediately switch to
            enemy.SwitchState(enemy.ChaseState);
        }

    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}
