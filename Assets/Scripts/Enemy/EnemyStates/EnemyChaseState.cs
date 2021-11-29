using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.attackBox.SetActive(false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);
        float step = enemy.moveSpeed * Time.deltaTime;

        if (enemy.transform.position.x < enemy.player.position.x)
        {
            //Enemy is to the left side of the player so move right
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.position, step);
            enemy.transform.localScale = new Vector2(1, 1);
        }
        if (enemy.transform.position.x > enemy.player.position.x)
        {
            //Enemy is to the left side of the player so move left
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.position, step);
            enemy.transform.localScale = new Vector2(-1, 1);
        }
        if(distToPlayer <= enemy.attackRange)
        {
            enemy.SwitchState(enemy.AttackState);
        }
        else if(distToPlayer >= enemy.agroRange)
        {
            enemy.SwitchState(enemy.ReturnHomeState);
        }

    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}
