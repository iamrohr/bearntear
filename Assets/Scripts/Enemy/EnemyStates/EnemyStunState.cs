using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("stunned");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {


    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }
}


