using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.attackBox.SetActive(false);
        enemy.animator.SetTrigger("Walk");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        
        Vector2 dir = enemy.player.transform.position - enemy.transform.position;
        enemy.rbHolder.velocity = dir.normalized * enemy.moveSpeed;

        if (dir.x < 0)
        {
            //Enemy is to the left side of the player so move right
            enemy.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(dir.x > 0)
        {
            enemy.transform.localScale = new Vector3(1, 1, 1);
        }

        if (distToPlayer <= enemy.attackRange)
        {
            enemy.rbHolder.velocity = Vector2.zero;
            enemy.SwitchState(enemy.AttackState);
        }
        if (distToPlayer >= enemy.agroRange)
        {
            enemy.rbHolder.velocity = Vector2.zero;
            enemy.SwitchState(enemy.ReturnHomeState);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }

}
