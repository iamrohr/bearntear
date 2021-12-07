using System;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.attackBox.SetActive(false);
        enemy.newEnemyPosition = enemy.EnemyRandPos(3);
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        float moveSpeed = 1;
        float step = moveSpeed * Time.deltaTime;
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.newEnemyPosition, step);

        if((enemy.newEnemyPosition - (Vector2)enemy.transform.position).magnitude < 0.1)
        {
            enemy.SwitchState(enemy.ReturnHomeState, 3);
        }
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }

}
