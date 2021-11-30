using UnityEngine;

public class EnemyReturnHomeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Dang, where did she go, I better get back home");
        enemy.attackBox.SetActive(false);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector2 currentposition = enemy.transform.position;

        float step = enemy.returnSpeed * Time.deltaTime;


        //Lägg till så den flippar sprite render. 

        //Gör en Gamecomponent som är public som är just den spriten.
        
        enemy.transform.localScale = new Vector2(1, 1);


        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.startPosition, step);

        if (currentposition.y == enemy.startPosition.y)
        {
            Debug.Log("Borde vara i idle nu");
            enemy.SwitchState(enemy.IdleState);
        }

    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}
