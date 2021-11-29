using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Hey, I am attacking this Teddy!");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.position);

        //enemy.attackBox.SetActive(true);


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
