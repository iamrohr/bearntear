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

        if (enemy.canAttackPlayer && distToPlayer < enemy.attackRange)
        {
            enemy.canAttackPlayer = false;

            enemy.StartCoroutine(enemy.AttackPlayer());
        }
        
        if (distToPlayer > enemy.attackRange)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }

}
