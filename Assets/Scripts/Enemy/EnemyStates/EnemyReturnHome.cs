using UnityEngine;

public class EnemyReturnHomeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.attackBox.SetActive(false);
        //Flips the enemy towards the start position.
        if (enemy.transform.position.x < enemy.enemyStartPosition.x)
        {
            enemy.transform.localScale = new Vector2(1, 1);
        }
        else
            enemy.transform.localScale = new Vector2(-1, 1);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector2 currentposition = enemy.transform.position;
        float step = enemy.moveSpeed * Time.deltaTime;
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.enemyStartPosition, step);

        if (currentposition.y == enemy.enemyStartPosition.y)
        {
            enemy.SwitchState(enemy.IdleState);
        }

        //Chase Player
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distToPlayer < enemy.agroRange)
        {
            enemy.SwitchState(enemy.IdleState, 3);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}
