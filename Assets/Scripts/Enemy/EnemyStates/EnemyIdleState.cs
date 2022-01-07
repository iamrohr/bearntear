using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        //Debug.Log("I am in idle");
        enemy.rbHolder.velocity *= 0;
        
        //Set all hurt animations to false
        for (int i = 0; i < enemy.animationHurt.Length; i++)
        {
            enemy.animationHurt[i].SetActive(false);

        }

        enemy.animator.SetTrigger("Idle");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float distToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);

        if (!enemy.enemyScript.infiniteChase)
        { 
            if (distToPlayer > enemy.agroRange)
            {
                enemy.SwitchState(enemy.PatrolState, 1.5f);
            }
            if (distToPlayer < enemy.agroRange)
            {
                enemy.SwitchState(enemy.ChaseState);
            }
        }
        else if (enemy.enemyScript.infiniteChase)
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


