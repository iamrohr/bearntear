using UnityEngine;

public class EnemyReturnHomeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.animator.SetTrigger("Walk");
        //enemy.attackBox.SetActive(false);
        //Flips the enemy towards the start position.
        if (enemy.transform.position.x < enemy.enemyStartPosition.x)
        {
            enemy.transform.localScale = new Vector2(1, 1);
        }
        else
            enemy.transform.localScale = new Vector2(-1, 1);

        Vector2 dir = enemy.enemyStartPosition - (Vector2)enemy.transform.position;
        enemy.rbHolder.velocity = dir.normalized * enemy.moveSpeed;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector2 currentposition = enemy.transform.position;

        if (enemy.enemyScript.infiniteChase)
        {
            enemy.SwitchState(enemy.infiniteChaseState);
        }

        if ((currentposition - enemy.enemyStartPosition).magnitude < 0.1f)
        {
            enemy.SwitchState(enemy.IdleState);
        }

        //Chase Player
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distToPlayer < enemy.agroRange)
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}
