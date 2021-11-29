using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Hello, I am the Enter state of the Idle state using the Enemy State Manager");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        if (distToPlayer > enemy.agroRange)
        {

        }
        else
        {
            //Immediately switch to
            enemy.SwitchState(enemy.ChaseState);
        }

    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}
