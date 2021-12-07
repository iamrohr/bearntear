using UnityEngine;

public class EnemyReturnHomeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.attackBox.SetActive(false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        Vector2 currentposition = enemy.transform.position;
        float step = enemy.returnSpeed * Time.deltaTime;

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.enemyStartPosition, step);

        //Flips the enemy towards the start position.
        if (enemy.transform.position.x < enemy.enemyStartPosition.x)
        {
            enemy.transform.localScale = new Vector2(1, 1);
        }
        else
            enemy.transform.localScale = new Vector2(-1, 1);


        if (currentposition.y == enemy.enemyStartPosition.y)
        {
            enemy.SwitchState(enemy.IdleState);
        }

    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}
