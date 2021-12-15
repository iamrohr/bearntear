using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("I am in stun");
        //enemy.rbHolder.velocity *= 0;
        enemy.animator.SetTrigger("Stunned");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {


    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}


